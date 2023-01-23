using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace DataLayer.Models
{
    [Serializable]
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Base64Content { get; set; }
        public bool IsRepresentative { get; set; }
        public bool IsNew { get; set; }
        public string ImageUrl { get; set; }

        public Image() { }

        public Image(HttpPostedFile file)
        {
            this.Name = file.FileName;
            using (var binaryReader = new BinaryReader(file.InputStream))
            {
                this.Base64Content = Convert.ToBase64String(binaryReader.ReadBytes(file.ContentLength));
            }
        }

        public void ImageToBase64(HttpPostedFile imageFile)
        {
            using (var binaryReader = new BinaryReader(imageFile.InputStream))
            {
                byte[] imageData = binaryReader.ReadBytes(imageFile.ContentLength);
                this.Base64Content = Convert.ToBase64String(imageData);
                this.Name = imageFile.FileName;
            }
        }

        public byte[] Base64ToByteArray()
        {
            return Convert.FromBase64String(this.Base64Content);
        }

        public override bool Equals(object obj)
        {
            return obj is Image image &&
                   Name == image.Name &&
                   ImageUrl == image.ImageUrl;
        }

        public override int GetHashCode()
        {
            int hashCode = -1246517319;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ImageUrl);
            return hashCode;
        }
    }
}

