namespace SimplifiedVisuals;

using System;
using Godot;

public static class NodeExtensions
{
    public static void ModifyChild<T>(this Node? instance, NodePath nodePath, Action<T> action) where T : Node
    {
        if (instance?.GetNodeOrNull<T>(nodePath) is not { } child) return;
        action(child);
    }

    public static void Modify<T>(this T? instance, Action<T> action) where T : Node
    {
        if (instance == null) return;
        action(instance);
    }

    public static void HideAndDisable(this Node instance, string pattern, bool remainVisible = false, bool printDebug = false)
    {
        foreach (var child in instance.FindChildren(pattern, recursive: true, owned: false))
        {
            if (child is not CanvasItem canvasItem) continue;
            Main.Logger.Debug($"Disabling node matching {pattern}: {child.Name} (type {child.GetType().Name})");
            canvasItem.ProcessMode = Node.ProcessModeEnum.Disabled;
            if (!remainVisible)
                canvasItem.Visible = false;
        }
    }
}