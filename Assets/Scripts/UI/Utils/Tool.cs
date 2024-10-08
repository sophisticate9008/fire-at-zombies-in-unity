using UnityEngine;

public class Tool
{
    public static Color LevelToColor(int level)
    {
        return level switch
        {
            1 => HexToColor("#787878"),
            2 => HexToColor("#4E8E00"),
            3 => HexToColor("#0F58D7"),
            4 => HexToColor("#A30ED7"),
            5 => HexToColor("#D77D0E"),
            6 => HexToColor("#D7290F"),
            7 => HexToColor("#E523E6"),
            _ => throw new System.NotImplementedException(),
        };

    }
    public static string LevelToColorString(int level)
    {
        return level switch
        {
            1 => "gray",
            2 => "green",
            3 => "blue",
            4 => "purple",
            5 => "orange",
            6 => "red",
            7 => "muticolour",
            _ => throw new System.NotImplementedException(),
        };
    }
    /// <summary>
    /// 将16进制颜色代码转换为Unity的Color对象
    /// 支持格式：#RRGGBB 或 #RRGGBBAA
    /// </summary>
    /// <param name="hex">16进制颜色字符串</param>
    /// <returns>解析后的Color对象</returns>
    public static Color HexToColor(string hex)
    {
        Color color;
        // 使用Unity内置的TryParseHtmlString来解析16进制字符串
        if (ColorUtility.TryParseHtmlString(hex, out color))
        {
            return color;
        }
        else
        {
            Debug.LogError("Invalid hex color code: " + hex);
            return Color.white; // 如果解析失败，返回默认的白色
        }
    }
}