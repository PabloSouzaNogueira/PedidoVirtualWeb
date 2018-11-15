using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PedidoVirtualWeb.Classes
{
    public class FilesHelper
    {
        public static bool UploadPhoto(HttpPostedFileBase file, string folder, string name)
        {
            string path = string.Empty;

            if (file == null || string.IsNullOrEmpty(file.FileName) || string.IsNullOrEmpty(folder))
                return false;

            try
            {
                if (file != null)
                {
                    path = Path.Combine(HttpContext.Current.Server.MapPath(folder), name);
                    file.SaveAs(path);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}