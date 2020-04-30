using System;

namespace ExeInjector.Helper
{
    internal class MenuItem
    {
        internal string Label { get; }
        internal Action Action { get; }
        internal string Description { get; }
        internal int LabelLength { get; }

        public MenuItem(string label, string description, Action action)
        {
            Label = label;
            LabelLength = label.Length;
            Description = description;
            Action = action;
        }
    }
}