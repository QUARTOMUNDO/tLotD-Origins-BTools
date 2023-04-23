using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using System.Diagnostics;

public class CreatureEditorSettings : MonoBehaviour
{
    public TMP_InputField DefaultsPath;
    public TMP_InputField SourcePath;
    public TMP_InputField ExportPath;

    private void OnEnable()
    {
        DefaultsPath.text = PortController.single.defaultXML_Path;
        SourcePath.text = PortController.single.sourceXML_Path;
        ExportPath.text = PortController.single.exportXML_Path;

        DefaultsPath.onEndEdit.AddListener(DefaultsPathChangedResponse);
        SourcePath.onEndEdit.AddListener(SourcePathChangedResponse);
        ExportPath.onEndEdit.AddListener(ExportPathChangedResponse);

    }

    private void OnDisable()
    {
        DefaultsPath.onEndEdit.RemoveListener(DefaultsPathChangedResponse);
        SourcePath.onEndEdit.RemoveListener(SourcePathChangedResponse);
        ExportPath.onEndEdit.RemoveListener(ExportPathChangedResponse);
    }

    public void DefaultsPathChangedResponse(string newPath)
    {
        if (System.IO.File.Exists(newPath)) PortController.single.defaultXML_Path = newPath;
        else
        {
            UnityEngine.Debug.LogError("Failed to change defaults path, xml file does not exist on path");
        }
    }

    public void SourcePathChangedResponse(string newPath)
    {

        if (System.IO.File.Exists(newPath)) PortController.single.sourceXML_Path = newPath;
        else
        {
            UnityEngine.Debug.LogError("Failed to change source path, xml file does not exist on path");
        }
    }

    public void ExportPathChangedResponse(string newPath)
    {

        if (newPath.EndsWith(".xml")) PortController.single.exportXML_Path = newPath;
        else
        {
            UnityEngine.Debug.LogError("Failed to change export path, make sure the path ends with the desired file name with .xml at the end");
        }
    }

    public void ReloadFiles()
    {
        PortController.single.RequestCreatureReload();
    }

    public void BrowseToDefaults()
    {
        //ProcessStartInfo processStartInfo = new ProcessStartInfo("explorer.exe", Application.persistentDataPath);
        //Process process = Process.Start(processStartInfo);
        //System.Windows.Forms.OpenFileDialog ofd;
        //if (ofd.ShowDialog() == DialogResult.OK)
        //{
        //    string File = ofd.FileName;
        //}
    }

    public void BrowseToSource()
    {

    }

    public void BrowseToExport()
    {

    }

}
