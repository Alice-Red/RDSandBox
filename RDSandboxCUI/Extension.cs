using System.Collections.Generic;
using System.IO;

namespace RDSandboxCUI
{
    public class Extension
    {
        public static Dictionary<string, string> ToContentType = new Dictionary<string, string>() {
            {"html", "text/html"},
            {"htm", "text/html"},
            {"tex", "application/x-latex"},
            {"latex", "application/x-latex"},
            {"ltx", "application/x-latex"},
            {"pdf", "application/pdf"},
            {"rtf", "application/rtf"},
            {"sgm", "text/sgml"},
            {"sgml", "text/sgml"},
            {"tab", "text/tab-separated-values"},
            {"tsv", "text/tab-separated-values"},
            {"txt", "text/plain"},
            {"xml", "text/xml"},
            {"jar", "application/java-archiver"},
            {"cpt", "application/mac-compactpro"},
            {"gz", "application/gzip"},
            {"hqx", "application/mac-binhex40"},
            {"sh", "application/x-sh"},
            {"shar", "application/x-sh"},
            {"sit", "application/x-stuffit"},
            {"tar", "application/x-tar"},
            {"z", "application/x-compress"},
            {"zip", "application/zip"},
            {"ai", "application/postscript"},
            {"bmp", "image/x-bmp"},
            {"rle", "image/x-bmp"},
            {"dib", "image/x-bmp"},
            {"cgm", "image/cgm"},
            {"dwf", "drawing/x-dwf"},
            {"epsf", "appilcation/postscript"},
            {"eps", "appilcation/postscript"},
            {"ps", "appilcation/postscript"},
            {"fif", "image/fif"},
            {"fpx", "image/fpx"},
            {"gif", "image/gif"},
            {"jpg", "image/jpeg"},
            {"jpeg", "image/jpeg"},
            {"jpe", "image/jpeg"},
            {"jfif", "image/jpeg"},
            {"jfi", "image/jpeg"},
            {"pcd", "image/pcd"},
            {"pict", "image/pict"},
            {"pct", "image/pict"},
            {"png", "image/x-png"},
            {"tga", "image/x-targa"},
            {"tpic", "image/x-targa"},
            {"vda", "image/x-targa"},
            {"vst", "image/x-targa"},
            {"tiff", "image/tiff"},
            {"tif", "image/tiff"},
            {"wrl", "model/vrml"},
            {"xbm", "image/x-bitmap"},
            {"xpm", "image/x-xpixmap"},
            {"aiff", "audio/aiff"},
            {"aif", "audio/aiff"},
            {"au", "audio/basic"},
            {"kar", "audio/midi"},
            {"m1a", "audio/mpeg"},
            {"m2a", "audio/mpeg"},
            {"midi", "audio/midi"},
            {"mid", "audio/midi"},
            {"smf", "audio/midi"},
            {"mp2", "audio/mpeg"},
            {"mp3", "audio/mpeg"},
            {"mpa", "audio/mpeg"},
            {"mpega", "audio/mpeg"},
            {"rpm", "audio/x-pn-realaudio-plugin"},
            {"snd", "audio/basic"},
            {"swa", "application/x-director"},
            {"vqf", "audio/x-twinvq"},
            {"wav", "audio/wav"},
            {"aab", "application/x-authorware-bin"},
            {"aam", "application/x-authorware-map"},
            {"aas", "application/x-authorware-seg"},
            {"asf", "video/x-ms-asf"},
            {"avi", "vide/x-msvideo"},
            {"dcr", "application/x-director"},
            {"dir", "application/x-director"},
            {"dxr", "application/x-director"},
            {"flc", "video/flc"},
            {"fli", "video/flc"},
            {"mng", "video/mng"},
            {"m1s", "vide/mpeg"},
            {"m1v", "vide/mpeg"},
            {"m2s", "vide/mpeg"},
            {"m2v", "vide/mpeg"},
            {"moov", "video/quicktime"},
            {"mov", "video/quicktime"},
            {"qt", "video/quicktime"},
            {"mpeg", "vide/mpeg"},
            {"mpg", "vide/mpeg"},
            {"mpe", "vide/mpeg"},
            {"mpv", "vide/mpeg"},
            {"ppt", "application/mspowerpoint"},
            {"rm", "audio/x-pn-realaudio"},
            {"spl", "application/futuresplash"},
            {"swf", "application/x-shockwave-flash"},
            {"vdo", "video/vdo"},
            {"viv", "video/vnd.vivo"},
            {"vivo", "video/vnd.vivo"},
            {"xdm", "application/x-xdma"},
            {"xdma", "application/x-xdma"},
            {"cdf", "application/x-netcdf"},
            {"class", "application/octet-stream"},
            {"exe", "application/exe"},
            {"pl", "application/x-perl"},
            {"ram", "audio/x-pn-realaudio"},
            {"vdb", "application/activexdocument"},
            {"vqe", "audio/x-twinvq-plugin"},
            {"vql", "audio/x-twinvq-plugin"},
        };
    }
}