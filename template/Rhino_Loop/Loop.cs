using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Rhino;
//using Rhino.Geometry;

// namespace なので名前空間と言われています。プログラムの結界みたいなものです。
// この結界の外からはRhino_Loop.と毎回付けていかなければなりません。
// ただし、上のusing 〇〇として予め書くとその名前空間にあるものは省略可能です。
namespace Rhino_Loop 
{
    class Loop : Rhino_Processing //このLoopというクラスはRhino_Processingというクラスを継承しているという意味です。
    {

        // public は　パブリック空間と同じようにだれでもアクセス出来るという意味です。反対に、privateも後で出てきます。
        // overrideは"上に載せる"という意味です。「既にある関数を上書きするよ」という意味です。Rhino_Processing.csを探すとあります。
        // 建築でも何もない空間をヴォイドといいますが、似たような意味です。実行しても何も返しませんという意味です。
        // SetupとDrawはこの本のために作った名前です。
        public override void Setup() //この関数は最初に一度だけ実行されます。
        {
            
        }

        public override void Draw() //　この関数は何度も何度も（ユーザーが止めるまで）繰り返し実行されます
        {
            
        }


    }
}
