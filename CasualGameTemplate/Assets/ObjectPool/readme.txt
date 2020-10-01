■このオブジェクトプールの特徴
よくあるオブジェクトを非アクティブ状態にしてプール。
プレハブのInstanceIDで管理するのでリストの登録が不要。
片方向リンクリストなので高速。
導入時のスクリプトの変更が少ない。
プール時だけ管理する為、普通にDestroyしても影響なし。
シーンに直接配置した場合はプールせずにDestroyする。
プレハブごとに初期化とプール時のイベントを登録。
オブジェクトプールマネージャのサンプルを付属。

■ファイル構成
ObjectPool.cs ：オブジェクトプール本体のスクリプト(シングルトン)
ObjectPoolItem.cs ：プールしたいプレハブのルートに追加するスクリプト
ObjectPoolManager_sample.cs ：プールする数を調整するスクリプトのサンプル
readme.txt ：オブジェクトプールの説明

■使い方
１、プレハブにObjectPoolItemコンポーネントを追加する。

２、プレハブ生成はusing A_rosuko.ObjectPool;を追加して
Instantiate(プレハブ, pos , qt);を下記に書き換えてください。
ObjectPool.Instance.GetGameObject(プレハブ , pos , qt);

３、プールはusing A_rosuko.ObjectPool;を追加して
Destroy(gameObject);を下記に書き換える。
ObjectPool.Instance.SleepGameObject(ObjectPoolItem);

※ObjectPool.Instance.SleepGameObject(gameObject)でも動作しますが、
内部でGetConponent<ObjectPoolItem>()しているのでObjectPoolItemをキャッシュして渡す方が速いです。

４、初期化などが必要であればエディタのインスペクターからOnEventWakeUpやOnEventSleepイベントに関数を登録してください。

■仕組み
オブジェクトプール本体のDictionaryでリンクリストの先頭を作成する。
DictionaryのキーはプレハブのInstanceIDとする。
各オブジェクトのObjectPoolItemコンポーネント側で接続するリンクリストの先頭をキャッシュしておき、先頭の次に接続する。
プールから取り出す時はリンクリストの先頭の次から取り出し、その次を先頭に接続しなおす。

■保証及び責任、著作権について
本ソフトウェアは無保証です。
本ソフトウェアの使用を通して生じたいかなる損害、不利益に対しても、制作者は責任を負わないものとします。

ObjactPool：Copyright 2018 A_rosuko
Released under the MIT license