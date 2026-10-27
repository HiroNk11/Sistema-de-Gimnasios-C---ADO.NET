"""Regenera el manual sin nombres de perfiles ni metadatos personales. Requiere reportlab."""
from pathlib import Path
from xml.sax.saxutils import escape
from reportlab.pdfgen import canvas
from reportlab.platypus import SimpleDocTemplate, Paragraph, Spacer, PageBreak
from reportlab.lib.styles import ParagraphStyle
from reportlab.lib.colors import HexColor
from reportlab.lib.enums import TA_LEFT

ROOT=Path(__file__).resolve().parents[1]
class CanvasManual(canvas.Canvas):
    def __init__(self,*a,**kw):
        super().__init__(*a,**kw)
        self.setTitle('Manual de uso - Body Sport Gym')
        self.setAuthor('Proyecto Body Sport Gym')
        self.setSubject('Operación y protección de datos; ejemplos sin datos personales')
    def showPage(self):
        self.saveState()
        self.setFont('Helvetica',9)
        self.setFillColor(HexColor('#526377'))
        self.drawString(44,28,'BODY SPORT GYM  |  Manual de uso')
        self.drawRightString(550,28,str(self.getPageNumber()))
        self.restoreState()
        super().showPage()

def generar():
    styles={
        'h1':ParagraphStyle('h1',fontName='Helvetica-Bold',fontSize=23,leading=28,textColor=HexColor('#15304F'),spaceAfter=24),
        'h2':ParagraphStyle('h2',fontName='Helvetica-Bold',fontSize=15,leading=20,textColor=HexColor('#146D74'),spaceBefore=15,spaceAfter=12),
        'p':ParagraphStyle('p',fontName='Helvetica',fontSize=10.5,leading=16,textColor=HexColor('#25364A'),spaceAfter=11,alignment=TA_LEFT),
    }
    story=[]
    for bloque in (ROOT/'docs/ManualUsuario.md').read_text(encoding='utf-8-sig').split('\n\n'):
        bloque=bloque.strip()
        if not bloque:continue
        if bloque=='---':story.append(PageBreak());continue
        kind='h1' if bloque.startswith('# ') else 'h2' if bloque.startswith('## ') else 'p'
        texto=bloque[2:] if kind=='h1' else bloque[3:] if kind=='h2' else bloque
        story.append(Paragraph(escape(texto).replace('\n',' '),styles[kind]))
    out=ROOT/'CapaIU/Documentacion/ManualUsuario.pdf'
    SimpleDocTemplate(str(out),pagesize=(595.28,841.89),leftMargin=44,rightMargin=44,topMargin=40,bottomMargin=52).build(story,canvasmaker=CanvasManual)
    print('Manual regenerado.')
if __name__=='__main__':generar()
