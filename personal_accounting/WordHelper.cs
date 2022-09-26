using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace personal_accounting
{
    class WordHelper
    {
        private FileInfo _fileInfo { get; set; }
        public WordHelper(string fileName)
        {
            if (File.Exists(fileName))
            {
                _fileInfo = new FileInfo(fileName);
            }
            else
            {
                Console.WriteLine("Не найден файл");
            }
        }

        internal bool Process(Dictionary<string, string> items)
        {
            Word.Application app = null;
            try
            {
                app = new Word.Application();
                Object file = _fileInfo.FullName;
                Object missing = Type.Missing;
                app.Documents.Open(file);
                foreach (var item in items)
                {
                    Word.Find find = app.Selection.Find;
                    find.Text = item.Key;
                    find.Replacement.Text = item.Value;
                    Object wrap = Word.WdFindWrap.wdFindContinue;
                    Object replace = Word.WdReplace.wdReplaceAll;
                    find.Execute(FindText: Type.Missing,
                        MatchCase: false,
                        MatchWholeWord: false,
                        MatchSoundsLike: missing,
                        MatchAllWordForms: false,
                        Forward: true,
                        Wrap: wrap,
                        Format: false,
                        ReplaceWith: missing, Replace: replace);
                }
                Object newFileName = Path.Combine(DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss ") + _fileInfo.Name);
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = Path.Combine(DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss ") + _fileInfo.Name); ;
                dlg.DefaultExt = ".doc";
                dlg.Filter = "Text documents (.doc)|*.doc";

                Nullable<bool> result = dlg.ShowDialog();


                if (result == true)
                {
                    newFileName = dlg.FileName;
                }
                app.ActiveDocument.SaveAs2(newFileName);
                app.ActiveDocument.Close();
                app.Quit();
                return true;
            }
            catch (Exception ex)

            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (app != null)
                    app.Quit();
            }

        }
    }
}
