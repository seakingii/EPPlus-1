/*************************************************************************************************
  Required Notice: Copyright (C) EPPlus Software AB. 
  This software is licensed under PolyForm Noncommercial License 1.0.0 
  and may only be used for noncommercial purposes 
  https://polyformproject.org/licenses/noncommercial/1.0.0/

  A commercial license to use this software can be purchased at https://epplussoftware.com
 *************************************************************************************************
  Date               Author                       Change
 *************************************************************************************************
  01/27/2020         EPPlus Software AB       Initial release EPPlus 5
 *************************************************************************************************/
using OfficeOpenXml.Drawing.Style;
using OfficeOpenXml.Drawing.Style.Font;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace OfficeOpenXml.Drawing.Theme
{
    /// <summary>
    /// A collection of fonts in a theme
    /// </summary>
    public class ExcelThemeFontCollection : XmlHelper, IEnumerable<ExcelDrawingFontBase>
    {
        List<ExcelDrawingFontBase> _lst = new List<ExcelDrawingFontBase>();
        internal ExcelThemeFontCollection(XmlNamespaceManager nameSpaceManager, XmlNode topNode) : base(nameSpaceManager,topNode)
        {
            foreach(XmlNode node in topNode.ChildNodes)
            {
                if(node.LocalName=="font")
                {
                    _lst.Add(new ExcelDrawingFont(nameSpaceManager, node));
                }
                else
                {
                    _lst.Add(new ExcelDrawingFontSpecial(nameSpaceManager, node));
                }
            }
        }
        /// <summary>
        /// The collection index
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns></returns>
        public ExcelDrawingFontBase this[int index]
        {
            get
            {
                return (_lst[index]);
            }
        }
        /// <summary>
        /// Adds a normal font to the collection
        /// </summary>
        /// <param name="typeface">The typeface, or name of the font</param>
        /// <param name="script">The script, or language, in which the typeface is supposed to be used</param>
        /// <returns>The font</returns>
        public ExcelDrawingFont Add(string typeface, string script)
        {
            XmlNode e=TopNode.OwnerDocument.CreateElement("a","font",ExcelPackage.schemaDrawings);
            TopNode.AppendChild(e);
            var f = new ExcelDrawingFont(NameSpaceManager, e) { Typeface=typeface, Script=script };
            _lst.Add(f);
            return f;
        }
        /// <summary>
        /// Adds a special font to the fonts collection
        /// </summary>
        /// <param name="type">The font type</param>
        /// <param name="typeface">The typeface, or name of the font</param>
        /// <returns>The font</returns>
        public ExcelDrawingFontSpecial AddSpecialFont(eFontType type, string typeface)
        {
            string typeName;
            switch (type)
            {
                case eFontType.Complex:
                    typeName = "cs";
                    break;
                case eFontType.EastAsian:
                    typeName = "ea";
                    break;
                case eFontType.Latin:
                    typeName = "latin";
                    break;
                case eFontType.Symbol:
                    typeName = "sym";
                    break;
                default:
                    throw (new ArgumentException("Please use the Add method to add normal fonts"));
            }
            XmlNode e = TopNode.OwnerDocument.CreateElement("a", typeName, ExcelPackage.schemaDrawings);
            TopNode.AppendChild(e);
            var f = new ExcelDrawingFontSpecial(NameSpaceManager, e) { Typeface=typeface };
            _lst.Add(f);
            return f;
        }

        /// <summary>
        /// Number of items in the collection
        /// </summary>
        public int Count
        {
            get
            {
                return _lst.Count;
            }
        }
        /// <summary>
        /// Gets an enumerator for the collection
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<ExcelDrawingFontBase> GetEnumerator()
        {
            return _lst.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _lst.GetEnumerator();
        }
    }
}