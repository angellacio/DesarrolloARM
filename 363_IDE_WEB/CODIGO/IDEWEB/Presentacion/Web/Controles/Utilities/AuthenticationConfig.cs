//@(#)SCADE2(W:SKDN09071PC4:Sat.Scade.Net.IDE.Presentacion.Web:AuthenticationConfig:0:11/Febrero/2009[Sat.Scade.Net.IDE.Presentacion.Web:0:11/Febrero/2009]) 
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using System.Collections;

namespace Sat.Scade.Net.IDE.Presentacion.Web
{
    internal static class AuthenticationConfig
    {
        // Methods
        internal static bool AccessingLoginPage(HttpContext context, string loginUrl)
        {
            if (!string.IsNullOrEmpty(loginUrl))
            {
                loginUrl = GetCompleteLoginUrl(context, loginUrl);
                if (string.IsNullOrEmpty(loginUrl))
                {
                    return false;
                }
                int index = loginUrl.IndexOf('?');
                if (index >= 0)
                {
                    loginUrl = loginUrl.Substring(0, index);
                }
                string path = context.Request.Path;
                if (AuthenticationConfig.EqualsIgnoreCase(path, loginUrl))
                {
                    return true;
                }
                if (loginUrl.IndexOf('%') >= 0)
                {
                    string str2 = HttpUtility.UrlDecode(loginUrl);
                    if (AuthenticationConfig.EqualsIgnoreCase(path, str2))
                    {
                        return true;
                    }
                    str2 = HttpUtility.UrlDecode(loginUrl, context.Request.ContentEncoding);
                    if (AuthenticationConfig.EqualsIgnoreCase(path, str2))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        internal static bool EqualsIgnoreCase(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1) && string.IsNullOrEmpty(s2))
            {
                return true;
            }
            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
            {
                return false;
            }
            if (s2.Length != s1.Length)
            {
                return false;
            }
            return (0 == string.Compare(s1, 0, s2, 0, s2.Length, StringComparison.OrdinalIgnoreCase));
        }

        internal static string GetCompleteLoginUrl(HttpContext context, string loginUrl)
        {
            if (string.IsNullOrEmpty(loginUrl))
            {
                return string.Empty;
            }
            if (UrlPath.IsRelativeUrl(loginUrl))
            {
                loginUrl = UrlPath.Combine(HttpRuntime.AppDomainAppVirtualPath, loginUrl);
            }
            return loginUrl;
        }
    }


    internal static class UrlPath
    {
        // Fields
        internal const char appRelativeCharacter = '~';
        internal const string appRelativeCharacterString = "~/";
        private const string dummyProtocolAndServer = "file://foo";
        private static char[] s_slashChars = new char[] { '\\', '/' };

        // Methods
        internal static string AppendSlashToPathIfNeeded(string path)
        {
            if (path == null)
            {
                return null;
            }
            int length = path.Length;
            if ((length != 0) && (path[length - 1] != '/'))
            {
                path = path + '/';
            }
            return path;
        }

        internal static void CheckValidVirtualPath(string path)
        {
            if (IsAbsolutePhysicalPath(path))
            {
                throw new HttpException("Physical_path_not_allowed");
            }
            if (path.IndexOf(':') >= 0)
            {
                throw new HttpException("Invalid_vpath");
            }
        }

        internal static string Combine(string basepath, string relative)
        {
            return Combine(HttpRuntime.AppDomainAppVirtualPath, basepath, relative);
        }

        private static string Combine(string appPath, string basepath, string relative)
        {
            string str;
            if (string.IsNullOrEmpty(relative))
            {
                throw new ArgumentNullException("relative");
            }
            if (string.IsNullOrEmpty(basepath))
            {
                throw new ArgumentNullException("basepath");
            }
            if ((basepath[0] == '~') && (basepath.Length == 1))
            {
                basepath = "~/";
            }
            else
            {
                int num = basepath.LastIndexOf('/');
                if (num < (basepath.Length - 1))
                {
                    basepath = basepath.Substring(0, num + 1);
                }
            }
            CheckValidVirtualPath(relative);
            if (IsRooted(relative))
            {
                str = relative;
            }
            else
            {
                if ((relative.Length == 1) && (relative[0] == '~'))
                {
                    return appPath;
                }
                if (IsAppRelativePath(relative))
                {
                    if (appPath.Length > 1)
                    {
                        str = appPath + "/" + relative.Substring(2);
                    }
                    else
                    {
                        str = "/" + relative.Substring(2);
                    }
                }
                else
                {
                    str = SimpleCombine(basepath, relative);
                }
            }
            return Reduce(str);
        }

        internal static string FixVirtualPathSlashes(string virtualPath)
        {
            virtualPath = virtualPath.Replace('\\', '/');
            while (true)
            {
                string str = virtualPath.Replace("//", "/");
                if (str == virtualPath)
                {
                    return virtualPath;
                }
                virtualPath = str;
            }
        }

        internal static string GetDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Empty_path_has_no_directory");
            }
            if ((path[0] != '/') && (path[0] != '~'))
            {
                throw new ArgumentException("Path_must_be_rooted");
            }
            if (path.Length == 1)
            {
                return path;
            }
            int num = path.LastIndexOf('/');
            if (num < 0)
            {
                throw new ArgumentException("Path_must_be_rooted");
            }
            return path.Substring(0, num + 1);
        }

        internal static string GetDirectoryOrRootName(string path)
        {
            string directoryName = Path.GetDirectoryName(path);
            if (directoryName == null)
            {
                directoryName = Path.GetPathRoot(path);
            }
            return directoryName;
        }

        internal static string GetExtension(string virtualPath)
        {
            return Path.GetExtension(virtualPath);
        }

        internal static string GetFileName(string virtualPath)
        {
            return Path.GetFileName(virtualPath);
        }

        internal static string GetFileNameWithoutExtension(string virtualPath)
        {
            return Path.GetFileNameWithoutExtension(virtualPath);
        }

        internal static bool HasTrailingSlash(string virtualPath)
        {
            return (virtualPath[virtualPath.Length - 1] == '/');
        }

        internal static bool IsAbsolutePhysicalPath(string path)
        {
            if ((path == null) || (path.Length < 3))
            {
                return false;
            }
            return (((path[1] == ':') && IsDirectorySeparatorChar(path[2])) || IsUncSharePath(path));
        }

        internal static bool IsAppRelativePath(string path)
        {
            if (path == null)
            {
                return false;
            }
            int length = path.Length;
            if (length == 0)
            {
                return false;
            }
            if (path[0] != '~')
            {
                return false;
            }
            if ((length != 1) && (path[1] != '\\'))
            {
                return (path[1] == '/');
            }
            return true;
        }

        private static bool IsDirectorySeparatorChar(char ch)
        {
            if (ch != '\\')
            {
                return (ch == '/');
            }
            return true;
        }

        internal static bool IsEqualOrSubpath(string path, string subpath)
        {
            if (!string.IsNullOrEmpty(path))
            {
                if (string.IsNullOrEmpty(subpath))
                {
                    return false;
                }
                int length = path.Length;
                if (path[length - 1] == '/')
                {
                    length--;
                }
                int num2 = subpath.Length;
                if (subpath[num2 - 1] == '/')
                {
                    num2--;
                }
                if (num2 < length)
                {
                    return false;
                }
                if (!EqualsIgnoreCase(path, 0, subpath, 0, length))
                {
                    return false;
                }
                if ((num2 > length) && (subpath[length] != '/'))
                {
                    return false;
                }
            }
            return true;
        }

        internal static bool EqualsIgnoreCase(string s1, int index1, string s2, int index2, int length)
        {
            return (string.Compare(s1, index1, s2, index2, length, StringComparison.OrdinalIgnoreCase) == 0);
        }

        internal static bool IsPathOnSameServer(string absUriOrLocalPath, Uri currentRequestUri)
        {
            Uri uri;
            if (Uri.TryCreate(absUriOrLocalPath, UriKind.Absolute, out uri) && !uri.IsLoopback)
            {
                return string.Equals(currentRequestUri.Host, uri.Host, StringComparison.OrdinalIgnoreCase);
            }
            return true;
        }

        internal static bool IsRelativeUrl(string virtualPath)
        {
            if (virtualPath.IndexOf(":", StringComparison.Ordinal) != -1)
            {
                return false;
            }
            return !IsRooted(virtualPath);
        }

        internal static bool IsRooted(string basepath)
        {
            if (!string.IsNullOrEmpty(basepath) && (basepath[0] != '/'))
            {
                return (basepath[0] == '\\');
            }
            return true;
        }

        internal static bool IsUncSharePath(string path)
        {
            return (((path.Length > 2) && IsDirectorySeparatorChar(path[0])) && IsDirectorySeparatorChar(path[1]));
        }

        internal static bool IsValidVirtualPathWithoutProtocol(string path)
        {
            if (path == null)
            {
                return false;
            }
            if (path.IndexOf(":", StringComparison.Ordinal) != -1)
            {
                return false;
            }
            return true;
        }        

        internal static string MakeVirtualPathAppAbsolute(string virtualPath)
        {
            return MakeVirtualPathAppAbsolute(virtualPath, HttpRuntime.AppDomainAppVirtualPath);
        }

        internal static string MakeVirtualPathAppAbsolute(string virtualPath, string applicationPath)
        {
            if ((virtualPath.Length == 1) && (virtualPath[0] == '~'))
            {
                return applicationPath;
            }
            if (((virtualPath.Length >= 2) && (virtualPath[0] == '~')) && ((virtualPath[1] == '/') || (virtualPath[1] == '\\')))
            {
                if (applicationPath.Length > 1)
                {
                    return (applicationPath + virtualPath.Substring(2));
                }
                return ("/" + virtualPath.Substring(2));
            }
            if (!IsRooted(virtualPath))
            {
                throw new ArgumentOutOfRangeException("virtualPath");
            }
            return virtualPath;
        }

        internal static string MakeVirtualPathAppAbsoluteReduceAndCheck(string virtualPath)
        {
            if (virtualPath == null)
            {
                throw new ArgumentNullException("virtualPath");
            }
            string str = Reduce(MakeVirtualPathAppAbsolute(virtualPath));
            if (!VirtualPathStartsWithAppPath(str))
            {
                throw new ArgumentException("Invalid_app_VirtualPath");
            }
            return str;
        }

        internal static string MakeVirtualPathAppRelative(string virtualPath)
        {
            return MakeVirtualPathAppRelative(virtualPath, HttpRuntime.AppDomainAppVirtualPath, false);
        }

        internal static bool StringStartsWithIgnoreCase(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
            {
                return false;
            }
            if (s2.Length > s1.Length)
            {
                return false;
            }
            return (0 == string.Compare(s1, 0, s2, 0, s2.Length, StringComparison.OrdinalIgnoreCase));
        }
        
        internal static string MakeVirtualPathAppRelative(string virtualPath, string applicationPath, bool nullIfNotInApp)
        {
            if (virtualPath == null)
            {
                throw new ArgumentNullException("virtualPath");
            }
            int length = applicationPath.Length;
            int num2 = virtualPath.Length;
            if ((num2 == (length - 1)) && StringStartsWithIgnoreCase(applicationPath, virtualPath))
            {
                return "~/";
            }
            if (!VirtualPathStartsWithVirtualPath(virtualPath, applicationPath))
            {
                if (nullIfNotInApp)
                {
                    return null;
                }
                return virtualPath;
            }
            if (num2 == length)
            {
                return "~/";
            }
            if (length == 1)
            {
                return ('~' + virtualPath);
            }
            return ('~' + virtualPath.Substring(length - 1));
        }

        internal static string MakeVirtualPathAppRelativeOrNull(string virtualPath)
        {
            return MakeVirtualPathAppRelative(virtualPath, HttpRuntime.AppDomainAppVirtualPath, true);
        }

        internal static bool PathEndsWithExtraSlash(string path)
        {
            if (path == null)
            {
                return false;
            }
            int length = path.Length;
            if ((length == 0) || (path[length - 1] != '\\'))
            {
                return false;
            }
            if ((length == 3) && (path[1] == ':'))
            {
                return false;
            }
            return true;
        }

        internal static bool PathIsDriveRoot(string path)
        {
            return (((path != null) && (path.Length == 3)) && ((path[1] == ':') && (path[2] == '\\')));
        }

        internal static string Reduce(string path)
        {
            string str = null;
            if (path != null)
            {
                int index = path.IndexOf('?');
                if (index >= 0)
                {
                    str = path.Substring(index);
                    path = path.Substring(0, index);
                }
            }
            path = FixVirtualPathSlashes(path);
            path = ReduceVirtualPath(path);
            if (str == null)
            {
                return path;
            }
            return (path + str);
        }

        internal static string ReduceVirtualPath(string path)
        {
            int length = path.Length;
            int startIndex = 0;
            while (true)
            {
                startIndex = path.IndexOf('.', startIndex);
                if (startIndex < 0)
                {
                    return path;
                }
                if (((startIndex == 0) || (path[startIndex - 1] == '/')) && ((((startIndex + 1) == length) || (path[startIndex + 1] == '/')) || ((path[startIndex + 1] == '.') && (((startIndex + 2) == length) || (path[startIndex + 2] == '/')))))
                {
                    break;
                }
                startIndex++;
            }
            ArrayList list = new ArrayList();
            StringBuilder builder = new StringBuilder();
            startIndex = 0;
            do
            {
                int num3 = startIndex;
                startIndex = path.IndexOf('/', num3 + 1);
                if (startIndex < 0)
                {
                    startIndex = length;
                }
                if ((((startIndex - num3) <= 3) && ((startIndex < 1) || (path[startIndex - 1] == '.'))) && (((num3 + 1) >= length) || (path[num3 + 1] == '.')))
                {
                    if ((startIndex - num3) == 3)
                    {
                        if (list.Count == 0)
                        {
                            throw new HttpException("Cannot_exit_up_top_directory");
                        }
                        if ((list.Count == 1) && IsAppRelativePath(path))
                        {
                            return ReduceVirtualPath(MakeVirtualPathAppAbsolute(path));
                        }
                        builder.Length = (int)list[list.Count - 1];
                        list.RemoveRange(list.Count - 1, 1);
                    }
                }
                else
                {
                    list.Add(builder.Length);
                    builder.Append(path, num3, startIndex - num3);
                }
            }
            while (startIndex != length);
            string str = builder.ToString();
            if (str.Length != 0)
            {
                return str;
            }
            if ((length > 0) && (path[0] == '/'))
            {
                return "/";
            }
            return ".";
        }

        internal static string RemoveSlashFromPathIfNeeded(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            int length = path.Length;
            if ((length > 1) && (path[length - 1] == '/'))
            {
                return path.Substring(0, length - 1);
            }
            return path;
        }

        internal static string SimpleCombine(string basepath, string relative)
        {
            if (HasTrailingSlash(basepath))
            {
                return (basepath + relative);
            }
            return (basepath + "/" + relative);
        }

        internal static bool VirtualPathStartsWithAppPath(string virtualPath)
        {
            return VirtualPathStartsWithVirtualPath(virtualPath, HttpRuntime.AppDomainAppVirtualPath);
        }

        private static bool VirtualPathStartsWithVirtualPath(string virtualPath1, string virtualPath2)
        {
            if (virtualPath1 == null)
            {
                throw new ArgumentNullException("virtualPath1");
            }
            if (virtualPath2 == null)
            {
                throw new ArgumentNullException("virtualPath2");
            }
            if (!StringStartsWithIgnoreCase(virtualPath1, virtualPath2))
            {
                return false;
            }
            int length = virtualPath2.Length;
            if (virtualPath1.Length != length)
            {
                if (length == 1)
                {
                    return true;
                }
                if (virtualPath2[length - 1] == '/')
                {
                    return true;
                }
                if (virtualPath1[length] != '/')
                {
                    return false;
                }
            }
            return true;
        }
    }



}
