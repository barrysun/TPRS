using System;

using org.in2bits.MyXls;
using System.IO;

public partial class Wacky : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XlsDocument xls = new XlsDocument();//�½�һ��xls�ĵ�
        xls.FileName = "Wacky.xls";//�趨�ļ���
        
        //Add some metadata (visible from Excel under File -> Properties)
        xls.SummaryInformation.Author = "Tim Erickson"; //���xls�ļ�������Ϣ
        xls.SummaryInformation.Subject = "A wacky display of Excel file generation";//����ļ�������Ϣ
        xls.DocumentSummaryInformation.Company = "in2bits.org";//����ļ���˾��Ϣ

        #region ���� 2008-05-17 ����


        string sheetName = "chc ʵ��";
        Worksheet sheet = xls.Workbook.Worksheets.AddNamed(sheetName);//�����Ϊ"chc ʵ��"��sheetҳ
        Cells cells = sheet.Cells;//Cellsʵ����sheetҳ�е�Ԫ��cell������
        //��Ԫ��1-base
        Cell cell = cells.Add(1, 2, "��");//�趨��һ�У��ڶ�����Ԫ���ֵ
        cell.HorizontalAlignment = HorizontalAlignments.Centered;//�趨���־���
        cell.Font.FontName = "��������";//�趨����
        cell.Font.Height = 20 * 20;//�趨�ִ�С�������С���� 1/20 point Ϊ��λ�ģ�
        cell.UseBorder = true;//ʹ�ñ߿�
        cell.BottomLineStyle = 2;//�趨�߿����Ϊ����
        cell.BottomLineColor = Colors.DarkRed;//�趨��ɫΪ����


        //cell�ĸ�ʽ�����Զ�����һ��xf������
        XF cellXF = xls.NewXF();//Ϊxls����һ��XFʵ����XF��cell��ʽ����
        cellXF.HorizontalAlignment = HorizontalAlignments.Centered;//�趨���־���
        cellXF.Font.FontName = "��������";//�趨����
        cellXF.Font.Height = 20 * 20;//�趨�ִ�С�������С���� 1/20 point Ϊ��λ�ģ�
        cellXF.UseBorder = true;//ʹ�ñ߿�
        cellXF.BottomLineStyle = 2;//�趨�߿����Ϊ����
        cellXF.BottomLineColor = Colors.DarkRed;//�趨��ɫΪ����
        
        cell = cells.AddValueCellXF(2, 2,"��", cellXF);//���趨�õĸ�ʽ���cell

        cellXF.Font.FontName = "����_GB2312";
        cell = cells.AddValueCellXF(3, 2, "��", cellXF);//��ʽ���Զ��ʹ��

        ColumnInfo colInfo = new ColumnInfo(xls, sheet);//�����и�ʽ����
        //�趨colInfo��ʽ�������õ���Ϊ��2�е���5��(�и�ʽΪ0-base)
        colInfo.ColumnIndexStart = 1;//��ʼ��Ϊ�ڶ���
        colInfo.ColumnIndexEnd = 5;//��ֹ��Ϊ������
        colInfo.Width = 15 * 256;//�еĿ�ȼ�����λΪ 1/256 �ַ���
        sheet.AddColumnInfo(colInfo);//�Ѹ�ʽ���ӵ�sheetҳ�ϣ�ע��AddColumnInfo�����е�С���⣬������colInfo�����θ���sheetҳ��
        colInfo.ColumnIndexEnd = 6;//���Ը����ж����ֵ
        ColumnInfo colInfo2 = new ColumnInfo(xls, sheet);//ͨ��������һ���и�ʽ���󣬲ŵ����趨�����п��
        colInfo2.ColumnIndexStart = 7;
        colInfo2.ColumnIndexEnd = 8;
        colInfo2.Width = 1 * 256;
        sheet.AddColumnInfo(colInfo2);

        MergeArea meaA = new MergeArea(1,2,3,4);//һ���ϲ���Ԫ��ʵ��(�ϲ���һ�С������� �� �ڶ��С�������)
        sheet.AddMergeArea(meaA);//��Ӻϲ���Ԫ��
        cellXF.VerticalAlignment=  VerticalAlignments.Centered;
        cellXF.Font.Height = 48 * 20;
        cellXF.Font.Bold = true;
        cellXF.Pattern = 3;//�趨��Ԫ�����������趨Ϊ0�����Ǵ�ɫ���
        cellXF.PatternBackgroundColor = Colors.DarkRed;//���ĵ�ɫ
        cellXF.PatternColor = Colors.DarkGreen;//�趨�����������ɫ
        cell = cells.Add(1, 3, "��",cellXF);

        #endregion



        for (int sheetNumber = 1; sheetNumber <= 5; sheetNumber++)
        {
            sheetName = "Sheet " + sheetNumber;
            int rowMin = sheetNumber;
            int rowCount = sheetNumber + 10;
            int colMin = sheetNumber;
            int colCount = sheetNumber + 10;
            sheet = xls.Workbook.Worksheets.AddNamed(sheetName);
            cells = sheet.Cells;
            for (int r = 0; r < rowCount; r++)
            {
                if (r == 0)
                {
                    for (int c = 0; c < colCount; c++)
                    {
                        cells.Add(rowMin + r, colMin + c, "Fld" + (c + 1)).Font.Bold = true;
                    }
                }
                else
                {
                    for (int c = 0; c < colCount; c++)
                    {
                        int val = r + c;
                        cell = cells.Add(rowMin + r, colMin + c, val);
                        if (val % 2 != 0)
                        {
                            cell.Font.FontName = "Times New Roman";
                            cell.Font.Underline = UnderlineTypes.Double;
                            cell.Rotation = 45; //�ַ���б45��
                        }
                    }
                }
            }
        }

        xls.Save(Server.MapPath("~/Admin/Excel"));
        string fileName = "Wacky.xls";//�ͻ��˱�����ļ���
        string filePath = Server.MapPath("~/Admin/Excel/Wacky.xls");//·��
        FileInfo fileInfo = new FileInfo(filePath);
        Response.Clear();
        Response.ClearContent();
        Response.ClearHeaders();
        Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
        Response.AddHeader("Content-Length", fileInfo.Length.ToString());
        Response.AddHeader("Content-Transfer-Encoding", "binary");
        Response.ContentType = "application/octet-stream";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
        Response.WriteFile(fileInfo.FullName);
        Response.Flush();
        try
        {
            File.Delete(filePath);
        }
        catch (Exception exx)
        {
            throw new Exception(exx.Message);
        }
        finally
        {
            Response.End();
        }
    }
}
