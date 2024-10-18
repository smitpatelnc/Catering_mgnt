﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CateringManagement.Models
{
    public class FileContent
    {
        [Key, ForeignKey("UploadedFile")]
        public int FileContentID { get; set; }

        [ScaffoldColumn(false)]
        public byte[] Content { get; set; }

        public UploadedFile UploadedFile { get; set; }
    }
}

