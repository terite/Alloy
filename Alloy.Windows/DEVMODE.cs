using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Alloy.Windows
{
	internal struct POINTL
	{
		public int x; 
		public int y;
	}

	[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
	struct DEVMODE
	{
		public const int CCHDEVICENAME = 32;
		public const int CCHFORMNAME = 32;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
		[System.Runtime.InteropServices.FieldOffset(0)]
		public string dmDeviceName;
		[System.Runtime.InteropServices.FieldOffset(32)]
		public Int16 dmSpecVersion;
		[System.Runtime.InteropServices.FieldOffset(34)]
		public Int16 dmDriverVersion;
		[System.Runtime.InteropServices.FieldOffset(36)]
		public Int16 dmSize;
		[System.Runtime.InteropServices.FieldOffset(38)]
		public Int16 dmDriverExtra;
		[System.Runtime.InteropServices.FieldOffset(40)]
		public Int32 dmFields;

		[System.Runtime.InteropServices.FieldOffset(44)]
		Int16 dmOrientation;
		[System.Runtime.InteropServices.FieldOffset(46)]
		Int16 dmPaperSize;
		[System.Runtime.InteropServices.FieldOffset(48)]
		Int16 dmPaperLength;
		[System.Runtime.InteropServices.FieldOffset(50)]
		Int16 dmPaperWidth;
		[System.Runtime.InteropServices.FieldOffset(52)]
		Int16 dmScale;
		[System.Runtime.InteropServices.FieldOffset(54)]
		Int16 dmCopies;
		[System.Runtime.InteropServices.FieldOffset(56)]
		Int16 dmDefaultSource;
		[System.Runtime.InteropServices.FieldOffset(58)]
		Int16 dmPrintQuality;

		[System.Runtime.InteropServices.FieldOffset(44)]
		public POINTL dmPosition;
		[System.Runtime.InteropServices.FieldOffset(52)]
		public Int32 dmDisplayOrientation;
		[System.Runtime.InteropServices.FieldOffset(56)]
		public Int32 dmDisplayFixedOutput;

		[System.Runtime.InteropServices.FieldOffset(60)]
		public short dmColor;
		[System.Runtime.InteropServices.FieldOffset(62)]
		public short dmDuplex;
		[System.Runtime.InteropServices.FieldOffset(64)]
		public short dmYResolution;
		[System.Runtime.InteropServices.FieldOffset(66)]
		public short dmTTOption;
		[System.Runtime.InteropServices.FieldOffset(68)]
		public short dmCollate;
		[System.Runtime.InteropServices.FieldOffset(72)]
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
		public string dmFormName;
		[System.Runtime.InteropServices.FieldOffset(102)]
		public Int16 dmLogPixels;
		[System.Runtime.InteropServices.FieldOffset(104)]
		public Int32 dmBitsPerPel;
		[System.Runtime.InteropServices.FieldOffset(108)]
		public Int32 dmPelsWidth;
		[System.Runtime.InteropServices.FieldOffset(112)]
		public Int32 dmPelsHeight;
		[System.Runtime.InteropServices.FieldOffset(116)]
		public Int32 dmDisplayFlags;
		[System.Runtime.InteropServices.FieldOffset(116)]
		public Int32 dmNup;
		[System.Runtime.InteropServices.FieldOffset(120)]
		public Int32 dmDisplayFrequency;
	}
}
