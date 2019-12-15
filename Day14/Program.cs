using System;
using System.Collections.Generic;
using System.Linq;

namespace Day14 {
  class Program {
    private static string inputTest = @"9 ORE => 2 A
8 ORE => 3 B
7 ORE => 5 C
3 A, 4 B => 1 AB
5 B, 7 C => 1 BC
4 C, 1 A => 1 CA
2 AB, 3 BC, 4 CA => 1 FUEL";
    private static string input2 = @"171 ORE => 8 CNZTR
7 ZLQW, 3 BMBT, 9 XCVML, 26 XMNCP, 1 WPTQ, 2 MZWV, 1 RJRHP => 4 PLWSL
114 ORE => 4 BHXH
14 VRPVC => 6 BMBT
6 BHXH, 18 KTJDG, 12 WPTQ, 7 PLWSL, 31 FHTLT, 37 ZDVW => 1 FUEL
6 WPTQ, 2 BMBT, 8 ZLQW, 18 KTJDG, 1 XMNCP, 6 MZWV, 1 RJRHP => 6 FHTLT
15 XDBXC, 2 LTCX, 1 VRPVC => 6 ZLQW
13 WPTQ, 10 LTCX, 3 RJRHP, 14 XMNCP, 2 MZWV, 1 ZLQW => 1 ZDVW
5 BMBT => 4 WPTQ
189 ORE => 9 KTJDG
1 MZWV, 17 XDBXC, 3 XCVML => 2 XMNCP
12 VRPVC, 27 CNZTR => 2 XDBXC
15 KTJDG, 12 BHXH => 5 XCVML
3 BHXH, 2 VRPVC => 7 MZWV
121 ORE => 7 VRPVC
7 XCVML => 6 RJRHP
5 BHXH, 4 VRPVC => 5 LTCX";
    private static string input = @"1 HJDM, 1 BMPDP, 8 DRCX, 2 TCTBL, 1 KGWDJ, 16 BRLF, 2 LWPB, 7 KDFQ => 6 ZSPL
1 PVRCK, 3 RSLR, 4 JBZD => 6 LCHRC
10 FCBVC, 1 TSJSJ, 20 SQCQ => 9 PNQLP
1 MBVL => 6 TSZJ
1 HWGQF => 4 ZSLVH
1 TBDSC, 13 TSZJ => 1 HRZH
1 RSLR, 1 LJWM => 3 RSFJR
1 VMZFB => 2 MBVL
4 DSTHJ, 2 TSZJ, 13 MBVL => 4 ZWLGK
1 MKTZ, 18 RVFJB, 1 RSLR, 2 HRZH, 14 ZWLGK, 4 RJFTV => 1 ZCVL
6 KDFQ, 1 PNQLP, 1 HRZH => 9 DLPMH
1 DSVT, 22 DRCX, 18 RJFTV, 2 MKTZ, 13 FVZBX, 15 SLTNZ, 7 ZSLVH => 5 GWJC
2 JZSJ, 3 ZSLVH, 6 HNRXC => 8 RJFTV
1 TSZJ => 7 GFVG
5 VMZFB => 4 JBZD
1 PBFZ, 23 JBZD, 2 LJWM => 1 TSJSJ
7 ZPQD => 7 VMZFB
2 LCHRC => 8 PXHK
2 TSZJ, 1 KCXMF, 1 FKJGC => 6 HWGQF
4 PBFZ => 1 FCBVC
1 GMWHM, 4 JQBKW => 8 SQCQ
5 SHMP => 5 PVRCK
10 KCXMF => 3 DRCX
15 VMZFB, 2 RSFJR => 6 KDFQ
35 HNRXC => 2 CJLG
8 MKTZ, 1 FCBVC, 12 HJDM => 9 BRLF
171 ORE => 8 GMWHM
8 RVFJB, 3 CJLG, 9 SLTNZ => 3 LWPB
1 PXHK, 2 RSFJR => 3 FVZBX
1 CJLG, 1 HRZH, 10 MKTZ => 8 KGWDJ
1 RSFJR => 3 FKJGC
1 NXCZM, 31 FKJGC => 2 MKTZ
18 XLWBP => 6 MBLWL
22 HNRXC => 8 FTGK
3 KGWDJ, 1 MLBJ, 5 HJDM => 7 DSVT
9 KDFQ => 5 NXCZM
2 RVFJB, 4 LGDKL, 1 PXHK => 5 CVTR
1 RSFJR, 6 GMWHM, 20 TSJSJ => 9 LGDKL
5 KCXMF => 9 RBDP
6 GWJC, 16 ZCVL, 29 JZSJ, 1 ZSPL, 35 MBLWL, 30 BWFRH, 2 MSFDB, 13 BMPDP, 11 FTGK, 1 ZWLGK => 1 FUEL
6 GFVG, 2 TVQP => 8 HJDM
1 CJLG, 13 PBFZ => 6 JZSJ
3 CVTR => 3 BMPDP
16 FPKMV, 1 ZSLVH => 8 MSFDB
9 JBZD, 12 LCHRC => 8 TBDSC
133 ORE => 3 LJWM
107 ORE => 7 SHMP
1 KDFQ, 1 LJWM => 9 FPKMV
3 PXHK => 4 BWFRH
123 ORE => 4 JQBKW
2 FVZBX, 1 JZSJ => 8 XLWBP
117 ORE => 2 ZPQD
7 NXCZM => 7 HNRXC
1 MLBJ, 22 RSLR => 8 KCXMF
2 TBDSC => 8 RVFJB
1 KDFQ, 23 DSTHJ => 7 SLTNZ
3 RSFJR => 6 MLBJ
5 PVRCK, 2 SQCQ => 9 RSLR
1 LGDKL, 17 MBVL, 6 PNQLP => 5 TVQP
3 RBDP => 6 TCTBL
1 DLPMH, 1 GFVG, 3 MBVL => 2 DSTHJ
21 VMZFB, 2 LJWM => 1 PBFZ";
    static void Main( string[] args ) {
      Console.WriteLine( "Hello World!" );

      var reactions = input.Split( "\n", StringSplitOptions.RemoveEmptyEntries ).Select( s => new Reaction( s ) )
        .ToDictionary( r => r.Result.Chemical );
      Dictionary<string, long> qtys = reactions.Values.SelectMany( r => r.Chemicals).Distinct().ToDictionary( s => s, s=> 0l);
      long nbOre = 1000000000000;
      bool RequestQtyChemical( string chemical, long Qty ) {
        if ( chemical == "ORE" ) {
          if ( Qty > nbOre ) {
            return false;
          }
          nbOre -= Qty;
          return true;
        }
        if ( qtys[chemical] >= Qty ) {
          qtys[chemical] -= Qty;
          return true;
        }
        Qty -= qtys[chemical];
        qtys[chemical] = 0;

        var reaction = reactions[chemical];
        //déterminons le ratio.
        long ratio = Qty / reaction.Result.Qty;
        if ( Qty % reaction.Result.Qty != 0 ) {
          ratio++;
        }

        foreach ( var ing in reaction.Ingredients ) {
          if ( !RequestQtyChemical( ing.Chemical, ing.Qty * ratio ) ) {
            return false;
          }
        }

        qtys[chemical] = ratio * reaction.Result.Qty - Qty;
        return true;
      }

      int nbFuel = 0;
      /*while ( RequestQtyChemical( "FUEL", 100 ) ) {
        nbFuel+=100;
        Console.WriteLine($"{nbFuel} - {nbOre}");
      }
      while ( RequestQtyChemical( "FUEL", 10) ) {
        nbFuel+=10;
        Console.WriteLine($"{nbFuel} - {nbOre}");
      }*/
      while ( RequestQtyChemical( "FUEL", 1 ) ) {
        nbFuel++;
        Console.WriteLine($"{nbFuel} - {nbOre}");
      }
    }

    static void Main1( string[] args ) {
      Console.WriteLine( "Hello World!" );

      var reactions = input.Split( "\n", StringSplitOptions.RemoveEmptyEntries ).Select( s => new Reaction( s ) )
        .ToDictionary( r => r.Result.Chemical );
      var qtys = reactions.Values.SelectMany( r => r.Chemicals).Distinct().ToDictionary( s => s, s=> 0);
      int nbOreRequested = 0;
      void RequestQtyChemical( string chemical, int Qty ) {
        if ( chemical == "ORE" ) {
          nbOreRequested += Qty;
          return;
        }
        if ( qtys[chemical] >= Qty ) {
          qtys[chemical] -= Qty;
          return;
        }
        Qty -= qtys[chemical];
        qtys[chemical] = 0;

        var reaction = reactions[chemical];
        //déterminons le ratio.
        int ratio = Qty / reaction.Result.Qty;
        if ( Qty % reaction.Result.Qty != 0 ) {
          ratio++;
        }

        foreach ( var ing in reaction.Ingredients ) {
          RequestQtyChemical( ing.Chemical, ing.Qty * ratio );
        }

        qtys[chemical] = ratio * reaction.Result.Qty - Qty;
      }
      RequestQtyChemical( "FUEL", 1 );
      Console.WriteLine(nbOreRequested);
    }



    public struct RecipeEntry {
      public string Chemical;
      public int Qty;

      public RecipeEntry( string chemical, int qty ) {
        Chemical = chemical;
        Qty = qty;
      }
      public RecipeEntry( string input) {
        string[] tmp = input.Split( " ", StringSplitOptions.RemoveEmptyEntries );

        Chemical = tmp[1].Trim();
        Qty = Int32.Parse( tmp[0].Trim() );
      }

      public override string ToString() {
        return $"{Qty} {Chemical}";
      }
    }

    public class Reaction {

      public RecipeEntry Result;
      public RecipeEntry[] Ingredients;

      public Reaction( string input ) {
        string[] leftright = input.Split( "=>", StringSplitOptions.RemoveEmptyEntries );
        Ingredients = leftright[0].Split( ",", StringSplitOptions.RemoveEmptyEntries )
          .Select( s => new RecipeEntry( s ) ).ToArray();
        Result = new RecipeEntry(leftright[1]);
      }

      public IEnumerable<string> Chemicals => new[] { Result.Chemical }.Concat( Ingredients.Select( i => i.Chemical ) );

      public override string ToString() {
        return $" {string.Join( ',', Ingredients) } => {Result}";
      }
    }
  }
}