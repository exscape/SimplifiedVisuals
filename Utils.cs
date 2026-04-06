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
}