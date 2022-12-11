# [WPF] TabControlEx

2022.12.11

`System.Windows.Controls.TabControl` の動作を改めて調査し直しました。 これまで雰囲気で使っちゃってました。

## System.Windows.Controls.TabControl

- TabItem を切り替える度に TabItem.Content が Load/Unload されます。
  (これがパフォーマンスに影響を与えるようです)
- 選択されてない TabItem.Content は VisualTree に表示されません。
- TabItem.Content のインスタンスは保持されます。
  (タブ選択の度に ctor が呼ばれることはありません)
- TabControl の読み込み時に全TabItemが Loaded されます。 ★こーゆーものらしい。

## ControlzEx.Controls.TabControlEx

 - 選択されてない TabItem.Content も VisualTree に保持されます。（サンプルだと ScrollViewer の位置が保持されます）
 - TabItem を切り替えても TabItem.Content に Unload が発行されません。
 - TabItem が初選択される時点で VisualTree に追加されて Loaded が発行されます。
 - TabControl の読み込み時に全TabItemが Loaded されます。 ★こーゆーものらしい。

## MahApps.Metro.Controls.MetroTabControl

- TabControlEx を継承しており VisualTree に TabItem を残すかを選択できます。`KeepVisualTreeInMemoryWhenChangingTabs="True"`
- TabItem を削除する機能が追加されています。
- TabControl の読み込み時に全TabItemが Loaded されます。 ★こーゆーものらしい。

## References

[ControlzEx/ControlzEx: Shared Controlz for WPF and ... more - GitHub](https://github.com/ControlzEx/ControlzEx)



[MahApps/MahApps.Metro: A framework that allows developers to cobble together a better UI for their own WPF applications with minimal effort. - GitHub](https://github.com/MahApps/MahApps.Metro)

なぜタブ再選択の Load/Unload で時間が掛かる？

→そーゆーもんで簡単な対策はない。



[How to stop Wpf Tabcontrol to unload Visual tree on Tab change - Stack Overflow](https://stackoverflow.com/questions/5038107/how-to-stop-wpf-tabcontrol-to-unload-visual-tree-on-tab-change)

> My usercontrols placed in tabs are unloaded and loaded every time the tabs are changed.
> It creates some unnecessary lag in the application.

ラグの数値はなかったけど、一般的に皆様お困りのご様子。



[validation - WPF TabControl: Load all tabs at window load - Stack Overflow](https://stackoverflow.com/questions/30337976/wpf-tabcontrol-load-all-tabs-at-window-load)

> why is it that after Ive selected each tab, going through them again still takes a long time between load/unload?



[【WPF】TabControlはお立ち台構造！？ | 創造的プログラミングと粘土細工](http://pro.art55.jp/?eid=962722)

> また、表示されていないTab内のアイテムと言うのはIsLoadedが不正確な状態です。
> 少なくともTabItemのうちコンテンツがIsLoadedがtrueのものが
> 選択されたタブで画面に表示されていると言うわけではないようです。
> WindowがLoadが完了した後は全てのTabItem.ContentはIsLoadedがtrueとなっています。
> その後、選択をかえるとUnLoadが走り、IsLoadedがfalseになっていきます。
> また、選択しなおせばLoadが走り、IsLoadedがtrueになります。

説明が分かりやすい。



[TabControlEx - Selected Item is recreated when I switch tab · Issue #159 · ControlzEx/ControlzEx](https://github.com/ControlzEx/ControlzEx/issues/159)

> That's because the style also sets the template.
> The template has to be different to work correctly.
> It has to expose a `Panel` with the name `PART_ItemsHolder` instead of a `ContentPresenter` with name `PART_SelectedContentHost` for content caching to work correctly and to prevent the default `TabControl` class from doing certain things.
> To fix your issue you will have to copy the template from MatrialDesign and modify it accordingly.

重要な回答と思うけど、どう対処すればよいか分からなかった。

