using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;

namespace Day12 {
  class Program {
    static void Main( string[] args ) {
      Console.WriteLine( "Hello World!" );


      //var moons = new[] { new Moon( -1, 0, 2 ), new Moon( 2, -10, -7 ), new Moon( 4, -8, 8 ), new Moon( 3, 5, -1 ) };

      var moons = new[] { new Moon( 1, 2, -9 ), new Moon( -1, -9, -4 ), new Moon( 17, 6, 8 ), new Moon( 12, 4, 2 ) };
      int nbSteps = 300000;

      int periodX = 0;
      for ( ; periodX < nbSteps; periodX++ ) {
        for ( int moonA = 0; moonA < moons.Length; moonA++ ) {
          for ( int moonB = moonA + 1; moonB < moons.Length; moonB++ ) {
            ApplyGravityX( moons[moonA], moons[moonB] );
          }
        }

        foreach ( var moon in moons ) {
          moon.ApplyVelocityX();
        }

        if ( moons.All( m =>  m.Velocity.X == 0 && m.Position.X == m.OriginalPosition.X )) {
          break;
        }
      }
      int periodY = 0;
      for ( ; periodY < nbSteps; periodY++ ) {
        for ( int moonA = 0; moonA < moons.Length; moonA++ ) {
          for ( int moonB = moonA + 1; moonB < moons.Length; moonB++ ) {
            ApplyGravityY( moons[moonA], moons[moonB] );
          }
        }

        foreach ( var moon in moons ) {
          moon.ApplyVelocityY();
        }

        if ( moons.All( m => m.Velocity.Y == 0 && m.Position.Y == m.OriginalPosition.Y ) ) {
          break;
        }
      }
      int periodZ = 0;
      for ( ; periodZ < nbSteps; periodZ++ ) {
        for ( int moonA = 0; moonA < moons.Length; moonA++ ) {
          for ( int moonB = moonA + 1; moonB < moons.Length; moonB++ ) {
            ApplyGravityZ( moons[moonA], moons[moonB] );
          }
        }

        foreach ( var moon in moons ) {
          moon.ApplyVelocityZ();
        }

        if ( moons.All( m => m.Velocity.Z == 0 && m.Position.Z == m.OriginalPosition.Z ) ) {
          break;
        }
      }
      Console.WriteLine($"periodX : { periodX +1}, periodY : { periodY+1}, periodZ : { periodZ+1}");
    }

    /*public static int LCM( int a, int b, int c ) {
      Math.Prim
    }*/

    public static bool SameState( (Position p, Velocity v)[] a, (Position p, Velocity v)[] b ) {
      for ( int i = 0; i < 4; i++ ) {
        if ( ( !a[i].p.Equals( b[i].p ) ) || ( !a[i].v.Equals( b[i].v ) ) ) {
          return false;
        }
      }
      return true;
    }

    static void Main1( string[] args ) {
      Console.WriteLine( "Hello World!" );

      //var moons = new[] { new Moon( -1, 0, 2 ), new Moon( 2, -10, -7 ), new Moon( 4, -8, 8 ), new Moon( 3, 5, -1 ) };
      var moons = new[] { new Moon( 1, 2, -9 ), new Moon( -1, -9, -4 ), new Moon( 17, 6, 8 ), new Moon( 12, 4, 2 ) };
      int nbSteps = 1000;
      for ( int i = 0; i < nbSteps; i++ ) {
        for ( int moonA = 0; moonA < moons.Length; moonA++ ) {
          for ( int moonB = moonA + 1; moonB < moons.Length; moonB++ ) {
            ApplyGravity( moons[moonA], moons[moonB] );
          }
        }

        Console.WriteLine( $"After {i + 1} steps: " );



        foreach ( var moon in moons ) {
          moon.ApplyVelocity();
          Console.WriteLine(moon);
        }

      }
      Console.WriteLine($"Total energy : { moons.Sum( m => m.TotalEnergy )}");
    }

    public class Position : IEquatable<Position> {
      public int X;
      public int Y;
      public int Z;

      public Position( int x, int y, int z ) {
        X = x;
        Y = y;
        Z = z;
      }

      public bool Equals( Position other ) {
        if ( ReferenceEquals( null, other ) )
          return false;
        if ( ReferenceEquals( this, other ) )
          return true;
        return X == other.X && Y == other.Y && Z == other.Z;
      }

      public override bool Equals( object obj ) {
        if ( ReferenceEquals( null, obj ) )
          return false;
        if ( ReferenceEquals( this, obj ) )
          return true;
        if ( obj.GetType() != this.GetType() )
          return false;
        return Equals( (Position) obj );
      }

      public override int GetHashCode() {
        unchecked {
          var hashCode = X;
          hashCode = ( hashCode * 397 ) ^ Y;
          hashCode = ( hashCode * 397 ) ^ Z;
          return hashCode;
        }
      }

      public Position( Position p ) : this( p.X, p.Y, p.Z){
      }

      public override string ToString() {
        return $"<X={X}, Y={Y}, Z={Z}>";
      }
    }

    public class Velocity : IEquatable<Velocity> {
      public int X;
      public int Y;
      public int Z;

      public override string ToString() {
        return $"<X={X}, Y={Y}, Z={Z}>";
      }

      public Velocity( int x, int y, int z ) {
        X = x;
        Y = y;
        Z = z;
      }

      public Velocity( Velocity v ) : this( v.X, v.Y, v.Z){
      }

      public bool Equals( Velocity other ) {
        if ( ReferenceEquals( null, other ) )
          return false;
        if ( ReferenceEquals( this, other ) )
          return true;
        return X == other.X && Y == other.Y && Z == other.Z;
      }

      public override bool Equals( object obj ) {
        if ( ReferenceEquals( null, obj ) )
          return false;
        if ( ReferenceEquals( this, obj ) )
          return true;
        if ( obj.GetType() != this.GetType() )
          return false;
        return Equals( (Velocity) obj );
      }

      public override int GetHashCode() {
        unchecked {
          var hashCode = X;
          hashCode = ( hashCode * 397 ) ^ Y;
          hashCode = ( hashCode * 397 ) ^ Z;
          return hashCode;
        }
      }
    }



    public static void ApplyGravity( Moon a, Moon b ) {
      ApplyGravityX( a, b );
      ApplyGravityY( a, b );
      ApplyGravityZ( a, b );
    }

    public static void ApplyGravityZ( Moon a, Moon b ) {
      if ( a.Position.Z > b.Position.Z ) {
        a.Velocity.Z--;
        b.Velocity.Z++;
      }
      else if ( a.Position.Z < b.Position.Z ) {
        a.Velocity.Z++;
        b.Velocity.Z--;
      }
    }

    public static void ApplyGravityY( Moon a, Moon b ) {
      if ( a.Position.Y > b.Position.Y ) {
        a.Velocity.Y--;
        b.Velocity.Y++;
      }
      else if ( a.Position.Y < b.Position.Y ) {
        a.Velocity.Y++;
        b.Velocity.Y--;
      }
    }

    public static void ApplyGravityX( Moon a, Moon b ) {
      if ( a.Position.X > b.Position.X ) {
        a.Velocity.X--;
        b.Velocity.X++;
      }
      else if ( a.Position.X < b.Position.X ) {
        a.Velocity.X++;
        b.Velocity.X--;
      }
    }

    public class Moon {
      public Position OriginalPosition;
      public Position Position;
      public Velocity Velocity = new Velocity( 0,0,0 );


      public Moon( int x, int y, int z ) {
        Position = new Position( x, y, z );
        OriginalPosition = new Position( Position );
      }

      public void ApplyVelocity() {
        ApplyVelocityX();
        ApplyVelocityY();
        ApplyVelocityZ();
      }

      public void ApplyVelocityZ() {
        Position.Z += Velocity.Z;
      }

      public void ApplyVelocityY() {
        Position.Y += Velocity.Y;
      }

      public void ApplyVelocityX() {
        Position.X += Velocity.X;
      }

      public override string ToString() {
        return $"Pos: {Position}, Vel: {Velocity}";
      }

      public int PotentialEnergy => Math.Abs( Position.X ) + Math.Abs( Position.Y ) + Math.Abs( Position.Z );
      public int KineticEnergy => Math.Abs( Velocity.X ) + Math.Abs( Velocity.Y ) + Math.Abs( Velocity.Z );

      public int TotalEnergy => PotentialEnergy * KineticEnergy;
    }
  }
}