﻿/*************************************************************************************************
  Required Notice: Copyright (C) EPPlus Software AB. 
  This software is licensed under PolyForm Noncommercial License 1.0.0 
  and may only be used for noncommercial purposes 
  https://polyformproject.org/licenses/noncommercial/1.0.0/

  A commercial license to use this software can be purchased at https://epplussoftware.com
 *************************************************************************************************
  Date               Author                       Change
 *************************************************************************************************
  04/15/2020         EPPlus Software AB       Initial release EPPlus 5
 *************************************************************************************************/
using OfficeOpenXml.Drawing.Chart.ChartEx;
using OfficeOpenXml.Packaging;
using OfficeOpenXml.Table.PivotTable;
using System;
using System.Xml;
namespace OfficeOpenXml.Drawing.Chart
{
    public class ExcelChartEx : ExcelChartBase
    {
        internal ExcelChartEx(ExcelDrawings drawings, XmlNode node, bool isPivot, ExcelGroupShape parent) : 
            base(drawings, node, GetChartType(node, drawings.NameSpaceManager), isPivot, parent, "mc:AlternateContent/mc:choice/xdr:graphicFrame")
        {
            _isChartEx = true;
        }

        private static eChartType GetChartType(XmlNode node, XmlNamespaceManager nsm)
        {
            var layoutId = node.SelectSingleNode("cx:plotArea/cx:plotAreaRegion/cx:series[1]/@layoutId", nsm);
            if (layoutId == null) throw new InvalidOperationException($"No series in chart"); 
            switch(layoutId.Value)
            {
                case "clusteredColumn":
                    return eChartType.Histogram;
                case "paretoLine":
                    return eChartType.Pareto;
                case "boxWhisker":
                    return eChartType.Boxwhisker;
                case "funnel":
                    return eChartType.Funnel;
                case "regionMap":
                    return eChartType.RegionMap;
                case "sunburst":
                    return eChartType.Sunburst;
                case "treemap":
                    return eChartType.Treemap;
                case "waterfall":
                    return eChartType.Waterfall;
                default:
                    throw new InvalidOperationException($"Unsupported layoutId in ChartEx Xml: {layoutId}");
            }          
        }

        internal override void AddAxis()
        {
            
        }

        internal ExcelChartEx(ExcelDrawings drawings, XmlNode drawingsNode, eChartType? type, ExcelPivotTable PivotTableSource, XmlDocument chartXml = null, ExcelGroupShape parent = null) :
            base(drawings, drawingsNode, type, null, PivotTableSource, chartXml, parent, "mc:AlternateContent/mc:choice/xdr:graphicFrame")
        {
            _isChartEx = true;
        }


        internal ExcelChartEx(ExcelDrawings drawings, XmlNode node, Uri uriChart, ZipPackagePart part, XmlDocument chartXml, XmlNode chartNode, ExcelGroupShape parent=null) :
            base(drawings, node, uriChart, part, chartXml, chartNode, parent, "mc:AlternateContent/mc:choice/xdr:graphicFrame")
        {
            _isChartEx = true;
            ChartType = GetChartType(chartNode, drawings.NameSpaceManager);
            Series.Init(this, drawings.NameSpaceManager, chartNode, false, Series._list);
        }
        /// <summary>
        /// Chart series
        /// </summary>
        public new ExcelChartSeries<ExcelChartExSerie> Series { get; } = new ExcelChartSeries<ExcelChartExSerie>();

        public override bool VaryColors
        {
            get 
            { 
                return false; 
            }
            set
            {
                throw new InvalidOperationException("VaryColors do not apply to Extended charts");
            }
        }
        //public eChartExType ChartType { get; set; }
        //public ExcelTextFont Font => throw new NotImplementedException();

        //public ExcelTextBody TextBody => throw new NotImplementedException();

        //public ExcelDrawingBorder Border => throw new NotImplementedException();

        //public ExcelDrawingEffectStyle Effect => throw new NotImplementedException();

        //public ExcelDrawingFill Fill => throw new NotImplementedException();

        //public ExcelDrawing3D ThreeD => throw new NotImplementedException();

        //public ExcelChartStyleManager StyleManager => throw new NotImplementedException();

        //public ExcelWorksheet WorkSheet => throw new NotImplementedException();

        //public XmlDocument ChartXml => throw new NotImplementedException();

        //public ExcelChartTitle Title => throw new NotImplementedException();

        //public bool HasTitle => throw new NotImplementedException();

        //public bool HasLegend => throw new NotImplementedException();

        //public ExcelChartSeries<ExcelChartSerie> Series => throw new NotImplementedException();

        //public ExcelChartAxis[] Axis => throw new NotImplementedException();

        //public ExcelChartAxis XAxis => throw new NotImplementedException();

        //public ExcelChartAxis YAxis => throw new NotImplementedException();

        //public bool UseSecondaryAxis => throw new NotImplementedException();

        //public eChartStyle Style => throw new NotImplementedException();

        //public bool RoundedCorners { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public bool ShowHiddenData { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public eDisplayBlanksAs DisplayBlanksAs { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //public bool ShowDataLabelsOverMaximum { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //public ExcelChartPlotArea PlotArea => throw new NotImplementedException();

        //public ExcelChartLegend Legend => throw new NotImplementedException();

        //public ExcelView3D View3D => throw new NotImplementedException();

        //public eGrouping Grouping => throw new NotImplementedException();

        //public bool VaryColors => throw new NotImplementedException();

        //public ExcelPivotTable PivotTableSource => throw new NotImplementedException();

        //ExcelPackage IPictureRelationDocument.Package => throw new NotImplementedException();

        //Dictionary<string, HashInfo> IPictureRelationDocument.Hashes => throw new NotImplementedException();

        //ZipPackagePart IPictureRelationDocument.RelatedPart => throw new NotImplementedException();

        //Uri IPictureRelationDocument.RelatedUri => throw new NotImplementedException();

        //eChartType IExcelChart.ChartType => throw new NotImplementedException();

        //public void CreatespPr()
        //{
        //    throw new NotImplementedException();
        //}

        //public void DeleteTitle()
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetMandatoryProperties()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
