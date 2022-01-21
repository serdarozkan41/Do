using Realms;
using System;

namespace Do.TikTokDownloader.Models
{
    public class FoundedVideo : RealmObject
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public string CoverImgUrl { get; set; }
        public string CleanUrl { get; set; }
        public string DownloadUrl { get; set; }
        public string DownloadedPath { get; set; }
        public DateTimeOffset? DownloadedDate { get; set; }
    }
}