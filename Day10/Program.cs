using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace Day10 {
  class Program {
    private static string inputTest = @".#..##.###...#######
##.############..##.
.#.######.########.#
.###.#######.####.#.
#####.##.#.##.###.##
..#####..#.#########
####################
#.####....###.#.#.##
##.#################
#####.##.###..####..
..######..##.#######
####.##.####...##..#
.#####..#.######.###
##...#.##########...
#.##########.#######
.####.#.###.###.#.##
....##.##.###..#####
.#.#.###########.###
#.#.#.#####.####.###
###.##.####.##.#..##
";
    private static string input = @".###.#...#.#.##.#.####..
.#....#####...#.######..
#.#.###.###.#.....#.####
##.###..##..####.#.####.
###########.#######.##.#
##########.#########.##.
.#.##.########.##...###.
###.#.##.#####.#.###.###
##.#####.##..###.#.##.#.
.#.#.#####.####.#..#####
.###.#####.#..#..##.#.##
########.##.#...########
.####..##..#.###.###.#.#
....######.##.#.######.#
###.####.######.#....###
############.#.#.##.####
##...##..####.####.#..##
.###.#########.###..#.##
#.##.#.#...##...#####..#
##.#..###############.##
##.###.#####.##.######..
##.#####.#.#.##..#######
...#######.######...####
#....#.#.#.####.#.#.#.##";

    static void Main( string[] args ) {
      Console.WriteLine( "Hello World!" );


      var map = new Map( input );

      List<int> asteroidsDestroyed = new List<int>();

      int centerAsteroid = ChoseAsteroid( map  );
      (int centerX, int centerY) = map.AbsoluteToCoordinates( centerAsteroid );

      Console.WriteLine( $"Max at { centerX}, {centerY} ({centerAsteroid})" );

      HashSet<int> aliveAsteroids = new HashSet<int>( map.Asteroids );
      while ( aliveAsteroids.Count > 1 ) {
        var asteroidVisibility = AsteroidVisibility( centerAsteroid, map, aliveAsteroids );
        HashSet<int> visibleAsteroids = asteroidVisibility.Where(
          kv => kv.Value == Visibility.Visible ).Select( kv => kv.Key ).ToHashSet();


        var drawMap = map.DrawMap( centerAsteroid, asteroidVisibility );
        Console.WriteLine(drawMap);

        //right top corner
        var rightTop = visibleAsteroids.Select( i => map.AbsoluteToCoordinates( i ) ).Where(
          i => i.X >= centerX && i.Y < centerY ).OrderByDescending( pair => pair, new Comparer( centerX, centerY )   ).ToArray();
        foreach ( (int X, int Y) point in rightTop ) {
          int ast = map.CoordinatesToAbsolute( point.X, point.Y );
          aliveAsteroids.Remove( ast );
          visibleAsteroids.Remove( ast );
          asteroidsDestroyed.Add(ast  );
        }

        //right bottom corner
        var rightBottom = visibleAsteroids.Select( i => map.AbsoluteToCoordinates( i ) ).Where(
          i => i.X > centerX && i.Y >= centerY ).OrderBy( pair => pair, new Comparer( centerX, centerY )   ).ToArray();
        foreach ( (int X, int Y) point in rightBottom ) {
          int ast = map.CoordinatesToAbsolute( point.X, point.Y );
          aliveAsteroids.Remove( ast );
          visibleAsteroids.Remove( ast );
          asteroidsDestroyed.Add(ast  );
        }

        //left bottem top corner
        var leftBottom = visibleAsteroids.Select( i => map.AbsoluteToCoordinates( i ) ).Where(
          i => i.X <= centerX && i.Y > centerY ).OrderByDescending( pair => pair, new Comparer( centerX, centerY )   ).ToArray();
        foreach ( (int X, int Y) point in leftBottom ) {
          int ast = map.CoordinatesToAbsolute( point.X, point.Y );
          aliveAsteroids.Remove( ast );
          visibleAsteroids.Remove( ast );
          asteroidsDestroyed.Add(ast  );
        }

        //left top corner
        var leftTop = visibleAsteroids.Select( i => map.AbsoluteToCoordinates( i ) ).Where(
          i => i.X < centerX && i.Y <= centerY ).OrderBy( pair => pair, new Comparer( centerX, centerY )   ).ToArray();
        foreach ( (int X, int Y) point in leftTop ) {
          int ast = map.CoordinatesToAbsolute( point.X, point.Y );
          aliveAsteroids.Remove( ast );
          visibleAsteroids.Remove( ast );
          asteroidsDestroyed.Add(ast  );
        }

      }

      int[] indices = new[] { 1, 2, 3, 10, 20, 50, 100, 199, 200, 201, 299 };
      foreach ( var index in indices ) {
        int aster = asteroidsDestroyed[index -1];
        Console.WriteLine( $"{ map.AbsoluteToCoordinates( aster ).X}, {map.AbsoluteToCoordinates( aster ).Y} ({aster})" );

      }



    }

    public class Comparer : IComparer<(int X, int Y)> {
      private int centerX;
      private int centerY;

      public Comparer( int centerX, int centerY ) {
        this.centerX = centerX;
        this.centerY = centerY;
      }

      public int Compare( (int X, int Y) pointA, (int X, int Y) pointB ) {

        int a = Math.Abs( pointA.Y - centerY );
        int b = Math.Abs( pointA.X - centerX );
        int c = Math.Abs( pointB.Y - centerY );
        int d = Math.Abs( pointB.X - centerX );

        long res = ( (long) a * (long) d ) - ( (long) b * (long) c );
        if ( res < 0 ) {
          return -1;
        }

        if ( res > 0 ) {
          return 1;
        }

        return 0;
      }
    }


    static int ChoseAsteroid( Map map ) {
      int max = Int32.MinValue;
      int maxX = -1;
      int maxY = -1;

      foreach ( int asteroid in map.Asteroids ) {
        var asteroidVisibility = AsteroidVisibility( asteroid, map );

        int nbVisible = asteroidVisibility.Values.Count( v => v == Visibility.Visible );
        ( int x, int y ) = map.AbsoluteToCoordinates( asteroid );

        if ( nbVisible > max ) {
          max = nbVisible;
          ( maxX, maxY ) = map.AbsoluteToCoordinates( asteroid );
        }
      }

      return map.CoordinatesToAbsolute( maxX, maxY );
    }

    private static Dictionary<int, Visibility> AsteroidVisibility( int asteroid, Map map, HashSet<int> asteroids = null ) {
      asteroids = asteroids ?? map.Asteroids;
      Dictionary<int, Visibility> asteroidVisibility =
        asteroids.Where( a => a != asteroid ).ToDictionary( a => a, a => Visibility.Visible );

      foreach ( var otherAsteroid in asteroids ) {
        if ( asteroid == otherAsteroid ) {
          continue;
        }

        foreach ( var invisibleAsteroid in map.InvisibleAsteroids( asteroid, otherAsteroid ) ) {
          asteroidVisibility[invisibleAsteroid] = Visibility.Invisible;
        }
      }

      return asteroidVisibility;
    }

    static void Main1( string[] args ) {
      Console.WriteLine( "Hello World!" );


      var map = new Map( input );

      int max = Int32.MinValue;
      int maxX = -1;
      int maxY = -1;

      foreach ( int asteroid in map.Asteroids ) {
        Dictionary<int, Visibility> asteroidVisibility = map.Asteroids.Where( a => a != asteroid ).ToDictionary( a => a, a => Visibility.Visible );

        foreach ( var otherAsteroid in map.Asteroids ) {
          if ( asteroid == otherAsteroid ) {
            continue;
          }

          foreach ( var invisibleAsteroid in map.InvisibleAsteroids( asteroid, otherAsteroid ) ) {
            asteroidVisibility[invisibleAsteroid] = Visibility.Invisible;
          }
        }

        int nbVisible = asteroidVisibility.Values.Count( v => v == Visibility.Visible );
        ( int x, int y ) = map.AbsoluteToCoordinates( asteroid );
        Console.WriteLine( $"computed {nbVisible} at { x}, {y} ({asteroid})" );
        if ( nbVisible == 32 ) {
          Console.WriteLine(map.DrawMap( asteroid, asteroidVisibility ));
        }
        if ( nbVisible > max ) {
          max = nbVisible;
          ( maxX, maxY ) = map.AbsoluteToCoordinates( asteroid );
        }
      }
      Console.WriteLine( $"Max {max} at { maxX}, {maxY}" );

    }



    public class Map {
      public int Width;
      public int Height;

      public HashSet<int> Asteroids = new HashSet<int>();

      public Map( string input ) {
        string[] lines = input.Split( "\n", StringSplitOptions.RemoveEmptyEntries );
         Height= lines.Length;
         Width = lines[0].Length;
        for ( int i = 0; i < lines.Length; i++ ) {
          string line = lines[i];
          for ( int j = 0; j < line.Length; j++ ) {
            if ( line[j] == '#' ) {
              Asteroids.Add( CoordinatesToAbsolute( j, i ) );
            }
          }
        }
      }

      public int CoordinatesToAbsolute( int X, int Y ) {
        return Y * Width + X;
      }
      public ( int X, int Y) AbsoluteToCoordinates( int absolute ) {
        return ( absolute % Width, absolute / Width );
      }

      public IEnumerable<(int X, int Y)> AsteroidsAsCoordinates() => Asteroids.Select( AbsoluteToCoordinates );

      public IEnumerable<int> InvisibleAsteroids( int fromAsteroid, int observedAsteroid, Predicate<int> AvailableAsteroid = null ) {
        AvailableAsteroid = AvailableAsteroid ?? ( i => Asteroids.Contains( i ) );
        ( int fromX, int fromY ) = AbsoluteToCoordinates( fromAsteroid );
        ( int toX, int toY ) = AbsoluteToCoordinates( observedAsteroid );
        int stepX = toX - fromX;
        int stepY = toY - fromY;

        if ( stepX == 0 ) {
          stepY /= Math.Abs(stepY);
        }
        else if ( stepY == 0 ) {
          stepX /= Math.Abs( stepX );
        }
        else {
          int divisor = GCD( Math.Abs( stepX ), Math.Abs(stepY) );
          stepX /= divisor;
          stepY /= divisor;
        }


        int nextX = toX;
        int nextY = toY;

        while ( true ) {
          nextX += stepX;
          nextY += stepY;
          if ( nextX < 0 || nextX >= Width || nextY < 0 || nextY >= Height ) {
            yield break;
          }

          var asteroid = CoordinatesToAbsolute( nextX, nextY );
          if( AvailableAsteroid(asteroid)){
            yield return asteroid;

          }
        }
      }

      public string DrawMap( int center, Dictionary<int, Visibility> asteroidVisibility ) {
        StringBuilder s = new StringBuilder();
        for ( int i = 0; i < Height; i++ ) {
          for ( int j = 0; j < Width; j++ ) {
            int asteroidNumber = CoordinatesToAbsolute( j, i );
            if ( center == asteroidNumber ) {
              s.Append( '0' );
              continue;
            }

            if ( asteroidVisibility.TryGetValue( asteroidNumber, out Visibility vis ) ) {
              if ( vis == Visibility.Invisible ) {
                s.Append( 'I' );
              }
              else {
                s.Append( '#' );
              }

              continue;
            }

            s.Append( '.' );

          }

          s.AppendLine();
        }

        return s.ToString();

      }
    }

    public enum Visibility {
      Exploded,
      Visible,
      Invisible
    }

    static int GCD(int p, int q)
    {
      if(q == 0)
      {
        return p;
      }

      int r = p % q;

      return GCD(q, r);
    }

  }

}