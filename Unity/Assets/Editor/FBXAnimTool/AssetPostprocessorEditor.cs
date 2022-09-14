using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;
using System.IO;
using System.Data;
using System.Diagnostics;
using ET;
using Excel;
using Debug = UnityEngine.Debug;

public class AssetPostprocessorEditor : AssetPostprocessor
{

    // private 
    public void OnComplete()
    {
        Debug.Log("On Complete");
    }
    public void OnPreprocessModel()
    {

        Debug.Log("OnPreprocessModel");
        //找当当前页面中的表格
        if (assetPath.IndexOf(".FBX") > -1)
        {
            var xlsxPath = assetPath.Replace(".FBX", ".xlsx");

            xlsxPath = GetAbsolutePath(xlsxPath);
            var rows = ReadExcel(xlsxPath);

            ModelImporter modelImporter = assetImporter as ModelImporter;

            List<ModelImporterClipAnimation> clipList = new List<ModelImporterClipAnimation>();

            var clipCount = modelImporter.clipAnimations.Length;

            if (clipCount > 1)
            {
                return;
            }

            for (var i = 0; i < rows.Count; i++)
            {
                DataRow dataRow = rows[i];
                var name = dataRow[0].ToString();
                // Log.Debug($"name {name}");
                Debug.Log($"name  {name}");
                if (name == "")
                {
                    break;
                }
                var startFrame = int.Parse(dataRow[1].ToString());
                var endFrame = int.Parse(dataRow[2].ToString());

                ModelImporterClipAnimation clip = new ModelImporterClipAnimation();
                clip.name = name;
                ModelImporterClipAnimation srcClip = null;
                for (var j = 0; j < clipCount; j++)
                {
                    var temp = modelImporter.clipAnimations[j];
                    if (temp.name == name)
                    {
                        srcClip = temp;
                    }
                }

                if (srcClip != null)
                {
                    clip.loop = srcClip.loop;
                    clip.wrapMode = WrapMode.Loop;
                }

                clip.firstFrame = startFrame;
                clip.lastFrame = endFrame;
                clipList.Add(clip);
            }
            modelImporter.clipAnimations = clipList.ToArray();
        }
        else
        {
            Debug.LogError("请先导入帧数对照表");
        }
    }
    public void OnPostprocessModel(GameObject go)
    {
    }
    private DataRowCollection ReadExcel(string _path, int _sheetIndex = 0)
    {
        FileStream stream = File.Open(_path, FileMode.Open, FileAccess.Read, FileShare.Read);
        //IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);//读取 Excel 1997-2003版本
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);//读取 2007及以后的版本
        DataSet result = excelReader.AsDataSet();
        return result.Tables[_sheetIndex].Rows;
    }
    public string GetRelativePath(string path)
    {
        string srcPath = path.Replace("\\", "/");
        var retPath = Regex.Replace(srcPath, @"\b.*Assets", "Assets");
        return retPath;
    }
    public string GetAbsolutePath(string path)
    {
        string srcPath = path.Replace("\\", "/");
        var retPath = Regex.Replace(srcPath, @"\b.*Assets", Application.dataPath);
        return retPath;
    }
}
