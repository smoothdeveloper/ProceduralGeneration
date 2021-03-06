﻿using System;

namespace CjClutter.OpenGl.Noise
{
    public class Musgrave
    {
        public struct Vector
        {
            public double X;
            public double Y;
            public double Z;
        }
        /*
         * Procedural fBm evaluated at "point"; returns value stored in "value".
         *
         * Copyright 1994 F. Kenton Musgrave 
         * 
         * Parameters:
         *    ``H''  is the fractal increment parameter
         *    ``lacunarity''  is the gap between successive frequencies
         *    ``octaves''  is the number of frequencies in the fBm
         */

        private double Noise3(Vector point)
        {
            var improvedPerlinNoise = new ImprovedPerlinNoise(4711);
            return improvedPerlinNoise.Noise(point.X, point.Y, point.Z);
        }

        //public double fBm(Vector point, double H, double lacunarity, double octaves)
        //{

        //    double value, frequency, remainder;
        //    bool first = true; //static
        //    double[] exponent_array = new double[] { }; //static

        //    /* precompute and store spectral weights */
        //    if (first)
        //    {
        //        /* seize required memory for exponent_array */
        //        exponent_array = new double[(int)(octaves + 1)];
        //        frequency = 1.0;
        //        for (i = 0; i <= octaves; i++)
        //        {
        //            /* compute weight for each frequency */
        //            exponent_array[i] = Math.Pow(frequency, -H);
        //            frequency *= lacunarity;
        //        }
        //        first = false;
        //    }

        //    value = 0.0;            /* initialize vars to proper values */
        //    frequency = 1.0;

        //    /* inner loop of spectral construction */
        //    int i;
        //    for (i = 0; i < octaves; i++)
        //    {
        //        value += Noise3(point) * exponent_array[i];
        //        point.X *= lacunarity;
        //        point.Y *= lacunarity;
        //        point.Z *= lacunarity;
        //    } /* for */

        //    remainder = octaves - (int)octaves;
        //    if (remainder > 0)      /* add in ``octaves''  remainder */
        //        /* ``i''  and spatial freq. are preset in loop above */
        //        value += remainder * Noise3(point) * exponent_array[i];

        //    return (value);

        //}

        #region multifractal

        ///*
        // * Procedural multifractal evaluated at "point"; 
        // * returns value stored in "value".
        // *
        // * Copyright 1994 F. Kenton Musgrave 
        // * 
        // * Parameters:
        // *    ``H''  determines the highest fractal dimension
        // *    ``lacunarity''  is gap between successive frequencies
        // *    ``octaves''  is the number of frequencies in the fBm
        // *    ``offset''  is the zero offset, which determines multifractality
        // */
        //double
        //multifractal( Vector point, double H, double lacunarity, 
        //              double octaves, double offset )
        //{
        //      double            value, frequency, remainder, Noise3();
        //      int               i;
        //      static int        first = TRUE;
        //      static double     *exponent_array;

        //      /* precompute and store spectral weights */
        //      if ( first ) {
        //            /* seize required memory for exponent_array */
        //            exponent_array = 
        //                        (double *)malloc( (octaves+1) * sizeof(double) );
        //            frequency = 1.0;
        //            for (i=0; i<=octaves; i++) {
        //                  /* compute weight for each frequency */
        //                  exponent_array[i] = pow( frequency, -H );
        //                  frequency *= lacunarity;
        //            }
        //            first = FALSE;
        //      }

        //      value = 1.0;            /* initialize vars to proper values */
        //      frequency = 1.0;

        //      /* inner loop of multifractal construction */
        //      for (i=0; i<octaves; i++) {
        //            value *= offset * frequency * Noise3( point );
        //            point.x *= lacunarity;
        //            point.y *= lacunarity;
        //            point.z *= lacunarity;
        //      } /* for */

        //      remainder = octaves - (int)octaves;
        //      if ( remainder )      /* add in ``octaves''  remainder */
        //            /* ``i''  and spatial freq. are preset in loop above */
        //            value += remainder * Noise3( point ) * exponent_array[i];

        //      return value;

        //} /* multifractal() */

        #endregion

        #region VLNoise3

        ///* "Variable Lacunarity Noise"  -or- VLNoise3()
        // * A distorted variety of Perlin noise.
        // *
        // * Copyright 1994 F. Kenton Musgrave 
        // */
        //double
        //VLNoise3( Vector point, double distortion )
        //{
        //        Vector offset, VecNoise3(), AddVectors();
        //        double Noise3();

        //        offset.x = point.x +0.5;        /* misregister domain */
        //        offset.y = point.y +0.5;
        //        offset.z = point.z +0.5;

        //        offset = VecNoise3( offset );   /* get a random vector */

        //        offset.x *= distortion;         /* scale the randomization */
        //        offset.y *= distortion;
        //        offset.z *= distortion;

        //        /* ``point'' is the domain; distort domain by adding ``offset'' */
        //        point = AddVectors( point, offset );

        //        return Noise3( point );         /* distorted-domain noise */

        //} /* VLNoise3() */

        #endregion

        #region Hetero_Terrain

        ///*
        // * Heterogeneous procedural terrain function: stats by altitude method.
        // * Evaluated at "point"; returns value stored in "value".
        // *
        // * Copyright 1994 F. Kenton Musgrave 
        // * 
        // * Parameters:
        // *       ``H''  determines the fractal dimension of the roughest areas
        // *       ``lacunarity''  is the gap between successive frequencies
        // *       ``octaves''  is the number of frequencies in the fBm
        // *       ``offset''  raises the terrain from `sea level'
        // */
        //double
        //Hetero_Terrain( Vector point,
        //            double H, double lacunarity, double octaves, double offset )
        //{
        //      double          value, increment, frequency, remainder, Noise3();
        //      int             i;
        //      static int      first = TRUE;
        //      static double   *exponent_array;

        //      /* precompute and store spectral weights, for efficiency */
        //      if ( first ) {
        //            /* seize required memory for exponent_array */
        //            exponent_array = 
        //                        (double *)malloc( (octaves+1) * sizeof(double) );
        //            frequency = 1.0;
        //            for (i=0; i<=octaves; i++) {
        //                  /* compute weight for each frequency */
        //                  exponent_array[i] = pow( frequency, -H );
        //                  frequency *= lacunarity;
        //            }
        //            first = FALSE;
        //      }

        //      /* first unscaled octave of function; later octaves are scaled */
        //      value = offset + Noise3( point );
        //      point.x *= lacunarity;
        //      point.y *= lacunarity;
        //      point.z *= lacunarity;

        //      /* spectral construction inner loop, where the fractal is built */
        //      for (i=1; i<octaves; i++) {
        //            /* obtain displaced noise value */
        //            increment = Noise3( point ) + offset;
        //            /* scale amplitude appropriately for this frequency */
        //            increment *= exponent_array[i];
        //            /* scale increment by current `altitude' of function */
        //            increment *= value;
        //            /* add increment to ``value''  */
        //            value += increment;
        //            /* raise spatial frequency */
        //            point.x *= lacunarity;
        //            point.y *= lacunarity;
        //            point.z *= lacunarity;
        //      } /* for */

        //      /* take care of remainder in ``octaves''  */
        //      remainder = octaves - (int)octaves;
        //      if ( remainder ) {
        //            /* ``i''  and spatial freq. are preset in loop above */
        //            /* note that the main loop code is made shorter here */
        //            /* you may want to that loop more like this */
        //            increment = (Noise3( point ) + offset) * exponent_array[i];
        //            value += remainder * increment * value;
        //      }

        //      return( value );

        //} /* Hetero_Terrain() */

        #endregion

        #region hybrid

        ///* Hybrid additive/multiplicative multifractal terrain model.
        // *
        // * Copyright 1994 F. Kenton Musgrave 
        // *
        // * Some good parameter values to start with:
        // *
        // *      H:           0.25
        // *      offset:      0.7
        // */
        //double 
        //HybridMultifractal( Vector point, double H, double lacunarity, 
        //                    double octaves, double offset )
        //{
        //      double          frequency, result, signal, weight, remainder; 
        //      double          Noise3();
        //      int             i;
        //      static int      first = TRUE;
        //      static double   *exponent_array;

        //      /* precompute and store spectral weights */
        //      if ( first ) {
        //            /* seize required memory for exponent_array */
        //            exponent_array = 
        //                        (double *)malloc( (octaves+1) * sizeof(double) );
        //            frequency = 1.0;
        //            for (i=0; i<=octaves; i++) {
        //                  /* compute weight for each frequency */
        //                  exponent_array[i] = pow( frequency, -H);
        //                  frequency *= lacunarity;
        //            }
        //            first = FALSE;
        //      }

        //      /* get first octave of function */
        //      result = ( Noise3( point ) + offset ) * exponent_array[0];
        //      weight = result;
        //      /* increase frequency */
        //      point.x *= lacunarity;
        //      point.y *= lacunarity;
        //      point.z *= lacunarity;

        //      /* spectral construction inner loop, where the fractal is built */
        //      for (i=1; i<octaves; i++) {
        //            /* prevent divergence */
        //            if ( weight > 1.0 )  weight = 1.0;

        //            /* get next higher frequency */
        //            signal = ( Noise3( point ) + offset ) * exponent_array[i];
        //            /* add it in, weighted by previous freq's local value */
        //            result += weight * signal;
        //            /* update the (monotonically decreasing) weighting value */
        //            /* (this is why H must specify a high fractal dimension) */
        //            weight *= signal;

        //            /* increase frequency */
        //            point.x *= lacunarity;
        //            point.y *= lacunarity;
        //            point.z *= lacunarity;
        //      } /* for */

        //      /* take care of remainder in ``octaves''  */
        //      remainder = octaves - (int)octaves;
        //      if ( remainder )
        //            /* ``i''  and spatial freq. are preset in loop above */
        //            result += remainder * Noise3( point ) * exponent_array[i];

        //      return( result );

        //} /* HybridMultifractal() */

        #endregion
    }
}