from pathlib import Path
import re
root=Path(__file__).resolve().parent.parent
base=(root/'Database/000_esquema_original.sql').read_text(encoding='utf-8-sig')
upgrade=(root/'Database/003_compatibilidad_DatabaseGym.sql').read_text(encoding='utf-8-sig')
new=(root/'Database/001_actualizacion.sql').read_text(encoding='utf-8-sig')
overrides=set(re.findall(r'CREATE OR ALTER (?:PROCEDURE|TRIGGER)\s+dbo\.(\w+)',upgrade,re.I))
parts=[]
for batch in re.split(r'^\s*GO\s*$',base,flags=re.M|re.I):
 batch=re.sub(r'/\*.*?\*/','',batch,flags=re.S).strip()
 if re.match(r'IF |SET |ALTER TABLE .* (?:CHECK CONSTRAINT|ENABLE TRIGGER)',batch,re.I|re.S):continue
 if not batch:continue
 name=re.search(r'CREATE\s+(?:PROC(?:EDURE)?|TRIGGER)\s+\[dbo\]\.\[([^]]+)\]',batch,re.I)
 if name and name.group(1).lower() in {x.lower() for x in overrides}:continue
 if batch.startswith('CREATE TABLE'):
  batch=batch.replace('[Email] [nvarchar](50)','[Email] [nvarchar](254)').replace('[Actividad] [nvarchar](50)','[Actividad] [nvarchar](512)')
  if '[dbo].[Bitacora](' in batch:batch=batch.replace('CONSTRAINT [PK_Bitacora]','[MomentoUtc] datetime2 NULL,\n CONSTRAINT [PK_Bitacora]')
  if '[dbo].[Reserva](' in batch:
   for a,b in [('[Id_Clase] [nchar](10)','[Id_Clase] int'),('[Fecha_Inicio] [nvarchar](50)','[Fecha_Inicio] datetime2'),('[Fecha_Fin] [nvarchar](50)','[Fecha_Fin] datetime2'),('[Precio_Clase] [int]','[Precio_Clase] decimal(18,2)'),('[Total] [int]','[Total] decimal(18,2)')]:batch=batch.replace(a,b)
  if '[dbo].[Socio_Clase](' in batch:batch=batch.replace('[Precio] [int]','[Precio] decimal(18,2)')
 parts.append(batch)
for m in re.finditer(r'CREATE TABLE dbo\.\w+\s*\(',new,re.I):
 start=m.start();i=m.end();depth=1
 while depth:
  if new[i]=='(':depth+=1
  elif new[i]==')':depth-=1
  i+=1
 parts.append(new[start:i]+';')
for batch in re.split(r'^\s*GO\s*$',new+'\nGO\n'+upgrade,flags=re.M|re.I):
 batch=re.sub(r'/\*.*?\*/','',batch,flags=re.S).strip()
 if re.match(r'CREATE OR ALTER (PROCEDURE|TRIGGER)',batch,re.I):parts.append(re.sub(r'CREATE OR ALTER','CREATE',batch,count=1,flags=re.I))
parts+=['CREATE SEQUENCE dbo.Seq_Usuario AS int START WITH 1 INCREMENT BY 1;','CREATE SEQUENCE dbo.Seq_Idioma AS int START WITH 1 INCREMENT BY 1;']
monitor=(root/'CapaRepositorio/MonitoreoRepository.cs').read_text(encoding='utf-8-sig')
for i,m in enumerate(re.finditer(r'@"((?:""|[^"])*)"',monitor)):
 sql=m.group(1).replace('""','"')
 if sql.lstrip().startswith('SELECT'):
  parts.append(f'CREATE PROCEDURE dbo.Prueba_Monitoreo_{i} @desde datetime2,@hasta datetime2 AS BEGIN\n{sql}\nEND;')
(root/'Tests/modelo_proyectado.sql').write_text('\nGO\n'.join(parts),encoding='utf-8-sig')
print(len(parts),'definiciones para validar modelo final')
