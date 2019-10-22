# MemoWebApp

このプロジェクトはASP.NET Core(MVC)の初歩学習のための教材サンプルです。

## 1.新規プロジェクトを作成する。

Visual Studio 2019 で ASP.NET Core MVC のプロジェクトを作成する。
事前に最新版までアップデーを行い、 .net core 3.0 フレームワークを利用すること。

## 2.HomeController と View の挙動。

空プロジェクトをデバッグすると既にデザインされているサンプルページが表示される。
HomeController の Index メソッド、 Privacy メソッドならびに、View/Home/Index.cshtml や Privacy.cshtml にブレイクポイントを貼って挙動を確かめよう。

また、ページリンクだけではなく URL を直接入力して通信のイメージを掴もう。

## 3.MemoControllerの作成。

Controllerフォルダ配下に MemoController クラスを作ろう。
※ 「新しい項目」で追加しないと余計なライブラリが追加されてしまうので注意。

また、Startup.csのendpointの設定も真似て作ろう。

```
// MemoMap
endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Memo}/{action=Index}/");
```

## 4.ControllerとViewの関係

Controllerだけ追加して、表示しようとすると次のエラーが発生する。

```
InvalidOperationException: The view 'Index' was not found. The following locations were searched: /Views/Memo/Index.cshtml /Views/Shared/Index.cshtml
```

これは Controller のメソッド名に対応する View が存在しないことが原因。
Views フォルダは以下にコントローラー名(Controllerを除く)のフォルダを作り、`メソッド名.cshtml` のファイル(Razorビュー)を作る。

## 5.View とレイアウトの関係

空のViewを作り表示させると、ファイルには何も書いてないのにヘッダーとフッターが表示される。
これは、Layoutという機能があり、規定で `_Layout.cshtml` が読み込まれるようになっている。

参考 : https://docs.microsoft.com/ja-jp/aspnet/core/mvc/views/layout?view=aspnetcore-3.0

## 6.新しくメモを作るためのページを作る。

コントローラーにNewメソッドを作る。
なお、今回はメソッドの規則を Ruby on Rails の Restful に沿って作っている。（わかりやすいから）

参考 : https://railsguides.jp/routing.html#crud%E3%80%81%E5%8B%95%E8%A9%9E%E3%80%81%E3%82%A2%E3%82%AF%E3%82%B7%E3%83%A7%E3%83%B3

New.cshtml の Razorビュー を作り、タイトルと本文があるフォームを作る。
なお、ASP.NET Core の規定デザインは Bootstrap4 を使っているのでそれを参考にする。

参考 : https://getbootstrap.jp/docs/4.1/components/forms/

※このタイミングでは form がどこに post するかは未定でOK。

## 7.メモを作る処理を作る。

1. コントローラーにCreateメソッドを作る。メソッドの引数に`title`と`text`を受け取るようにする。
2. 引数の情報でファイルを作る。

   ※ファイルを作るのは普通の.Netの知識で可能。
   ※拡張子は `.memo` とかにしておく。

3. View は「作成しました。」という結果を表示する。
4. URLにパラメータを付けて実行してみる。例、`/Memo/Create?title=test&text=hoobar`

## 8.New と Create を繋げる。

Newの画面のformでCreateを呼び出すように設定する。

まずはformタグに宛先を設定。「ASP.NET Core のフォームのタグ ヘルパー」を参考。

https://docs.microsoft.com/ja-jp/aspnet/core/mvc/views/working-with-forms?view=aspnetcore-3.0

次に、inputタグやtextareaタグのname属性を引数の名前にする。例、`name="title"`

上手く設定できていればCreateが呼ばれてメモが作られる。

## 9.一覧画面を作る。

Indexを改造。 `.memo` のファイル一覧を取得して `return View()` の引数に一覧を入れる。

Razorビューの先頭に下記を追加。

```
@model System.IO.FileInfo[]
```

次のようにループ文を書いてタグを書く。

```
@foreach(var file in Model)
{
    ここにHTMLタグを書いていく
}
```

※View引数のオブジェクトをRazorビューに渡す事ができる。`Model` で参照可能。

## 10.メモの内容表示画面を作る。

Showメソッドを作る。引数は `filename` とかでファイル名を受け取る。合わせてRazorビューも作る。

Showメソッド内でファイルを読み込み、 `Models.Memo.ShowModel` クラスを作って格納する。

ここでは、「タイトル」「本文」の2つをビューに渡す必要があるためモデルを作った。

## 11.一覧からリンクを張る

aタグを使ってリンクを張る。使うのは次の三つ。

* asp-controller

   コントローラー名の指定に使う。

* asp-action

   メソッド名の指定に使う。

* asp-route-???

   メソッドの引数の指定に使う。???には引数名を入れる。今回は`filename`

これでaタグの `href` 属性が作られる。

参照：https://docs.microsoft.com/ja-jp/aspnet/core/mvc/views/tag-helpers/built-in/anchor-tag-helper?view=aspnetcore-3.0


## 12.リンクや初期表示の調整

* `_Layout.cshtml` のヘッダー部分のリンクを修正する。

* `Startup.cs` の Route を修正

   HomeController は使わないのでコメントアウト。代わりにMemoを **default** にする。


## チャレンジ

ここまでで簡単なメモアプリを作ることが出来た。

しかし、次の点が特に不十分なので対処してみよう。

1. メモを編集する機能を付けてみよう。
2. メモを削除する機能を付けてみよう。
3. 異常系を考慮した作りにしてみよう。


---

Copyright 2019 kazuma.namiki
