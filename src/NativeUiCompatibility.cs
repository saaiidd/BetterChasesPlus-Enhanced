using System;
using System.Collections.Generic;
using System.Linq;
using LemonUI;
using LemonUI.Menus;

namespace NativeUI;

public delegate void ItemSelectEvent(UIMenu sender, UIMenuItem selectedItem, int index);
public delegate void CheckboxChangeEvent(UIMenu sender, UIMenuCheckboxItem checkboxItem, bool isChecked);
public delegate void ListChangedEvent(UIMenu sender, UIMenuListItem listItem, int newIndex);
public delegate void MenuCloseEvent(UIMenu sender);

public sealed class MenuPool
{
    private readonly ObjectPool pool = new ObjectPool();
    private readonly List<UIMenu> menus = new List<UIMenu>();
    public void Add(UIMenu menu)
    {
        if (menu == null) throw new ArgumentNullException(nameof(menu));
        if (menus.Contains(menu)) return;
        menus.Add(menu);
        pool.Add(menu.Inner);
    }
    public List<UIMenu> ToList() => menus.ToList();
    public void ProcessMenus() => pool.Process();
    public bool IsAnyMenuOpen() => pool.AreAnyVisible;
    public void CloseAllMenus() => pool.HideAll();
    public void RefreshIndex() { }
}

public class UIMenu
{
    private readonly List<UIMenuItem> items = new List<UIMenuItem>();
    internal NativeMenu Inner { get; }
    public bool Visible { get => Inner.Visible; set => Inner.Visible = value; }
    public event ItemSelectEvent OnItemSelect;
    public event CheckboxChangeEvent OnCheckboxChange;
    public event ListChangedEvent OnListChange;
    public event MenuCloseEvent OnMenuClose;
    public UIMenu(string title, string subtitle)
    {
        Inner = new NativeMenu(title, title, subtitle);
        Inner.Closed += (sender, args) => OnMenuClose?.Invoke(this);
    }
    public void AddItem(UIMenuItem item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));
        if (items.Contains(item)) return;
        items.Add(item);
        Inner.Add(item.Inner);
        item.Attach(this);
    }
    public void BindMenuToItem(UIMenu childMenu, UIMenuItem item)
    {
        if (childMenu == null) throw new ArgumentNullException(nameof(childMenu));
        if (item == null) throw new ArgumentNullException(nameof(item));
        childMenu.Inner.Parent = Inner;
        item.Inner.Activated += (sender, args) => { Inner.Visible = false; childMenu.Inner.Visible = true; };
    }
    public void Clear() { items.Clear(); Inner.Clear(); }
    internal int IndexOf(UIMenuItem item) => items.IndexOf(item);
    internal void RaiseItemSelect(UIMenuItem item) => OnItemSelect?.Invoke(this, item, IndexOf(item));
    internal void RaiseCheckboxChange(UIMenuCheckboxItem item) => OnCheckboxChange?.Invoke(this, item, item.Checked);
    internal void RaiseListChange(UIMenuListItem item) => OnListChange?.Invoke(this, item, item.Index);
}

public class UIMenuItem
{
    internal NativeItem Inner { get; }
    public bool Enabled { get => Inner.Enabled; set => Inner.Enabled = value; }
    public UIMenuItem(string title) : this(title, string.Empty) { }
    public UIMenuItem(string title, string description) : this(new NativeItem(title, description)) { }
    protected UIMenuItem(NativeItem item) { Inner = item ?? throw new ArgumentNullException(nameof(item)); }
    internal virtual void Attach(UIMenu owner) { Inner.Activated += (sender, args) => owner.RaiseItemSelect(this); }
}

public sealed class UIMenuCheckboxItem : UIMenuItem
{
    private NativeCheckboxItem Checkbox => (NativeCheckboxItem)Inner;
    public bool Checked { get => Checkbox.Checked; set => Checkbox.Checked = value; }
    public UIMenuCheckboxItem(string title, bool isChecked, string description)
        : base(new NativeCheckboxItem(title, description, isChecked)) { }
    internal override void Attach(UIMenu owner)
    {
        base.Attach(owner);
        Checkbox.CheckboxChanged += (sender, args) => owner.RaiseCheckboxChange(this);
    }
}

public sealed class UIMenuListItem : UIMenuItem
{
    private NativeListItem<object> List => (NativeListItem<object>)Inner;
    public int Index { get => List.SelectedIndex; set => List.SelectedIndex = value; }
    public UIMenuListItem(string title, IList<dynamic> items, int index, string description)
        : base(new NativeListItem<object>(title, description, items.Cast<object>().ToArray()))
    {
        if (items.Count > 0) Index = Math.Max(0, Math.Min(index, items.Count - 1));
    }
    public object IndexToItem(int index) => List.Items[index];
    internal override void Attach(UIMenu owner)
    {
        base.Attach(owner);
        List.ItemChanged += (sender, args) => owner.RaiseListChange(this);
    }
}
