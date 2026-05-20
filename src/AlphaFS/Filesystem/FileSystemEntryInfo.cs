/*  Copyright (C) 2008-2018 Peter Palotas, Jeffrey Jangli, Alexandr Normuradov
 *  
 *  Permission is hereby granted, free of charge, to any person obtaining a copy 
 *  of this software and associated documentation files (the "Software"), to deal 
 *  in the Software without restriction, including without limitation the rights 
 *  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
 *  copies of the Software, and to permit persons to whom the Software is 
 *  furnished to do so, subject to the following conditions:
 *  
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *  
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
 *  THE SOFTWARE. 
 */

using System;
using System.IO;
using System.Security;

namespace Alphaleonis.Win32.Filesystem
{
   /// <summary>Represents information about a file system entry.
   /// <para>This class cannot be inherited.</para>
   /// </summary>
   [Serializable]
   [SecurityCritical]
   public sealed class FileSystemEntryInfo : IEquatable<FileSystemEntryInfo>
   {
      #region Fields

      private string _fullPath;
      private string _longFullPath;
      private readonly bool _hasValidAttributes;

      #endregion // Fields


      #region Constructor

      /// <summary>Initializes a new instance of the <see cref="FileSystemEntryInfo"/> class.</summary>
      /// <param name="findData">The NativeMethods.WIN32_FIND_DATA structure.</param>
      internal FileSystemEntryInfo(NativeMethods.WIN32_FIND_DATA findData)
      {
         Win32FindData = findData;
         _hasValidAttributes = File.HasValidAttributes(findData.dwFileAttributes);
      }

      #endregion // Constructor


      #region Properties

      /// <summary>The instance 8.3 version of the filename.</summary>
      public string AlternateFileName
      {
         // This property is always empty when NativeMethods.FINDEX_INFO_LEVELS.Basic is used.

         get { return Win32FindData.cAlternateFileName; }
      }


      /// <summary>The instance attributes.</summary>
      public FileAttributes Attributes
      {
         get { return Win32FindData.dwFileAttributes; }
      }


      /// <summary>The instance creation time.</summary>
      public DateTime CreationTime
      {
         get { return CreationTimeUtc.ToLocalTime(); }
      }


      /// <summary>The instance creation time, in coordinated universal time (UTC).</summary>
      public DateTime CreationTimeUtc
      {
         get { return DateTime.FromFileTimeUtc(Win32FindData.ftCreationTime); }
      }


      /// <summary>The instance file extension.</summary>
      public string Extension
      {
         get { return Path.GetExtension(Win32FindData.cFileName, false); }
      }


      /// <summary>The instance file name.</summary>
      public string FileName
      {
         get { return Win32FindData.cFileName; }
      }


      /// <summary>The instance file size.</summary>
      public long FileSize
      {
         get { return NativeMethods.ToLong(Win32FindData.nFileSizeHigh, Win32FindData.nFileSizeLow); }
      }

      
      /// <summary>The instance full path.</summary>
      public string FullPath
      {
         get { return _fullPath; }

         set
         {
            _longFullPath = Path.GetLongPathCore(value, GetFullPathOptions.None);
            _fullPath = Path.GetRegularPathCore(_longFullPath, GetFullPathOptions.None, false);
         }
      }


      /// <summary>The instance is a candidate for backup or removal. </summary>
      public bool IsArchive => _hasValidAttributes && (Attributes & FileAttributes.Archive) != 0;


      /// <summary>The instance is compressed.</summary>
      public bool IsCompressed => _hasValidAttributes && (Attributes & FileAttributes.Compressed) != 0;


      /// <summary>Reserved for future use.</summary>
      public bool IsDevice => _hasValidAttributes && (Attributes & FileAttributes.Device) != 0;


      /// <summary>The instance is a directory.</summary>
      public bool IsDirectory => _hasValidAttributes && (Attributes & FileAttributes.Directory) != 0;


      /// <summary>The instance is encrypted. For a file, this means that all data in the file is encrypted. For a directory, this means that encryption is the default for newly created files and directories.</summary>
      public bool IsEncrypted => _hasValidAttributes && (Attributes & FileAttributes.Encrypted) != 0;


      /// <summary>The instance is hidden, and thus is not included in an ordinary directory listing.</summary>
      public bool IsHidden => _hasValidAttributes && (Attributes & FileAttributes.Hidden) != 0;


      /// <summary>The instance is a mount point. Applicable to local directories and local volumes.</summary>
      public bool IsMountPoint
      {
         get { return ReparsePointTag == ReparsePointTag.MountPoint; }
      }


      /// <summary>The instance is a standard file that has no special attributes. This attribute is valid only if it is used alone.</summary>
      public bool IsNormal => _hasValidAttributes && (Attributes & FileAttributes.Normal) != 0;


      /// <summary>The instance will not be indexed by the operating system's content indexing service.</summary>
      public bool IsNotContentIndexed => _hasValidAttributes && (Attributes & FileAttributes.NotContentIndexed) != 0;


      /// <summary>The instance is offline. The data of the file is not immediately available.</summary>
      public bool IsOffline => _hasValidAttributes && (Attributes & FileAttributes.Offline) != 0;


      /// <summary>The instance is read-only.</summary>
      public bool IsReadOnly => _hasValidAttributes && (Attributes & FileAttributes.ReadOnly) != 0;


      /// <summary>The instance contains a reparse point, which is a block of user-defined data associated with a file or a directory.</summary>
      public bool IsReparsePoint => _hasValidAttributes && (Attributes & FileAttributes.ReparsePoint) != 0;


      /// <summary>The instance is a sparse file. Sparse files are typically large files whose data consists of mostly zeros.</summary>
      public bool IsSparseFile => _hasValidAttributes && (Attributes & FileAttributes.SparseFile) != 0;


      /// <summary>The instance is a symbolic link.</summary>
      public bool IsSymbolicLink
      {
         get { return ReparsePointTag == ReparsePointTag.SymLink; }
      }


      /// <summary>The instance is a system file. That is, the file is part of the operating system or is used exclusively by the operating system.</summary>
      public bool IsSystem => _hasValidAttributes && (Attributes & FileAttributes.System) != 0;


      /// <summary>The instance is temporary. A temporary file contains data that is needed while an application is executing but is not needed after the application is finished.
      /// File systems try to keep all the data in memory for quicker access rather than flushing the data back to mass storage.
      /// A temporary file should be deleted by the application as soon as it is no longer needed.</summary>
      public bool IsTemporary => _hasValidAttributes && (Attributes & FileAttributes.Temporary) != 0;


      /// <summary>The instance time this entry was last accessed.</summary>
      public DateTime LastAccessTime
      {
         get { return LastAccessTimeUtc.ToLocalTime(); }
      }


      /// <summary>The instance time, in coordinated universal time (UTC), this entry was last accessed.</summary>
      public DateTime LastAccessTimeUtc
      {
         get { return DateTime.FromFileTimeUtc(Win32FindData.ftLastAccessTime); }
      }


      /// <summary>The instance time this entry was last modified.</summary>
      public DateTime LastWriteTime
      {
         get { return LastWriteTimeUtc.ToLocalTime(); }
      }


      /// <summary>The instance time, in coordinated universal time (UTC), this entry was last modified.</summary>
      public DateTime LastWriteTimeUtc
      {
         get { return DateTime.FromFileTimeUtc(Win32FindData.ftLastWriteTime); }
      }


      /// <summary>The instance full path in long path format.</summary>
      public string LongFullPath
      {
         get { return _longFullPath; }

         private set { _longFullPath = Path.GetLongPathCore(value, GetFullPathOptions.None); }
      }


      /// <summary>The instance reparse point tag.</summary>
      public ReparsePointTag ReparsePointTag
      {
         get { return IsReparsePoint ? Win32FindData.dwReserved0 : ReparsePointTag.None; }
      }


      /// <summary>The instance internal WIN32 FIND Data</summary>
      internal NativeMethods.WIN32_FIND_DATA Win32FindData { get; private set; }

      #endregion // Properties


      #region Methods

      /// <summary>Returns the <see cref="FullPath"/> of the FileSystemEntryInfo instance.</summary>
      /// <returns>Returns the <see cref="FullPath"/> of the FileSystemEntryInfo instance.</returns>
      public override string ToString()
      {
         return FullPath;
      }


      /// <summary>Serves as a hash function for a particular type.</summary>
      /// <returns>A hash code for the current Object.</returns>
      public override int GetHashCode()
      {
         return Utils.CombineHashCodesOf(FullPath, LongFullPath);
      }


      /// <summary>Determines whether the specified Object is equal to the current Object.</summary>
      /// <param name="other">Another <see cref="FileSystemInfo"/> instance to compare to.</param>
      /// <returns><c>true</c> if the specified Object is equal to the current Object; otherwise, <c>false</c>.</returns>
      public bool Equals(FileSystemEntryInfo other)
      {
         return null != other && GetType() == other.GetType() &&
                Equals(FileName, other.FileName) &&
                Equals(FullPath, other.FullPath) &&
                Equals(Attributes, other.Attributes) &&
                Equals(CreationTimeUtc, other.CreationTimeUtc) &&
                Equals(LastAccessTimeUtc, other.LastAccessTimeUtc);
      }


      /// <summary>Determines whether the specified Object is equal to the current Object.</summary>
      /// <param name="obj">Another object to compare to.</param>
      /// <returns><c>true</c> if the specified Object is equal to the current Object; otherwise, <c>false</c>.</returns>
      public override bool Equals(object obj)
      {
         var other = obj as FileSystemEntryInfo;

         return null != other && Equals(other);
      }


      /// <summary>Implements the operator ==</summary>
      /// <param name="left">A.</param>
      /// <param name="right">B.</param>
      /// <returns>The result of the operator.</returns>
      public static bool operator ==(FileSystemEntryInfo left, FileSystemEntryInfo right)
      {
         return ReferenceEquals(left, null) && ReferenceEquals(right, null) ||
                !ReferenceEquals(left, null) && !ReferenceEquals(right, null) && left.Equals(right);
      }


      /// <summary>Implements the operator !=</summary>
      /// <param name="left">A.</param>
      /// <param name="right">B.</param>
      /// <returns>The result of the operator.</returns>
      public static bool operator !=(FileSystemEntryInfo left, FileSystemEntryInfo right)
      {
         return !(left == right);
      }
      #endregion // Methods
   }
}
