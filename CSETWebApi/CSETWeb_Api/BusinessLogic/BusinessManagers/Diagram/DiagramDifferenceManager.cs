﻿using DataLayerCore.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace CSETWeb_Api.BusinessLogic.BusinessManagers.Diagram
{
    /// <summary>
    /// this is the main entry point for 
    /// diagram changes and differences
    /// </summary>
    public class DiagramDifferenceManager
    {
        private static readonly object _object = new object();

        private Dictionary<Guid, ComponentNode> OldDiagram = new Dictionary<Guid, ComponentNode>();
        private Dictionary<Guid, ComponentNode> NewDiagram = new Dictionary<Guid, ComponentNode>();

        /// <summary>
        /// pass in the xml document
        /// get the existing from the database
        /// create the dictionaries 
        /// call the diff extractor
        /// get back the adds and deletes
        /// add records to the table for the new components
        /// deleted records for the removed components
        /// </summary>
        public void buildDiagramDictionaries(XmlDocument newDiagramDocument, XmlDocument oldDiagramDocument)
        {
            NewDiagram = ProcessDiagram(newDiagramDocument);
            OldDiagram = ProcessDiagram(oldDiagramDocument);
        }

        public void SaveDifferences(CSET_Context context, int assessment_id)
        {
            bool _lockTaken = false;
            Monitor.Enter(_object, ref _lockTaken);
            try
            {
                lock (_object)
                {
                    DiagramDifferences differences = new DiagramDifferences();
                    differences.processComparison(this.NewDiagram, this.OldDiagram);
                    foreach (var newNode in differences.AddedNodes)
                    {
                        context.ASSESSMENT_DIAGRAM_COMPONENTS.Add(new ASSESSMENT_DIAGRAM_COMPONENTS()
                        {
                            Assessment_Id = assessment_id,
                            Component_Id = newNode.Key,
                            Diagram_Component_Type = newNode.Value.Component_Type,
                            DrawIO_id = newNode.Value.id,
                            label = newNode.Value.label
                        });
                    }
                    context.SaveChanges();

                    foreach (var deleteNode in differences.DeletedNodes)
                    {
                        var adc = context.ASSESSMENT_DIAGRAM_COMPONENTS
                            .FirstOrDefault(x => x.Assessment_Id == assessment_id && x.Component_Id == deleteNode.Key);
                        if (adc != null)
                        {
                            context.ASSESSMENT_DIAGRAM_COMPONENTS.Remove(adc);
                        }
                    }
                    context.SaveChanges();
                }
            }
            finally
            {
                System.Threading.Monitor.Exit(_object);
            }
        }

        private Dictionary<Guid, ComponentNode> ProcessDiagram(XmlDocument doc)
        {
            Dictionary<Guid, ComponentNode> nodesList = new Dictionary<Guid, ComponentNode>();
            XmlNodeList cells = doc.SelectNodes("/mxGraphModel/root/object");
            foreach (var c in cells)
            {
                var cell = (XmlElement)c;
                if (cell.HasAttribute("ComponentGuid"))
                {
                    ComponentNode cn = new ComponentNode();
                    foreach (XmlAttribute a in ((System.Xml.XmlElement)c).Attributes)
                    {
                        cn.setValue(a.Name, a.Value);
                    }
                    nodesList.Add(cn.ComponentGuid, cn);
                }
            }
            return nodesList;
        }
    }

    public class ComponentNode
    {
        public string label { get; set; }
        public Guid ComponentGuid { get; set; }
        public string id { get; set; }
        public string Component_Type { get; internal set; }

        public void setValue(string name, string value)
        {
            switch (name)
            {
                case "label":
                    this.label = value;
                    break;
                case "ComponentGuid":
                    this.ComponentGuid = new Guid(value);
                    break;
                case "id":
                    this.id = value;
                    break;
                case "Component_Type":
                    this.Component_Type = value;
                    break;
            }
        }
    }
}
