﻿// Accord Statistics Library
// The Accord.NET Framework
// http://accord-framework.net
//
// Copyright © César Souza, 2009-2014
// cesarsouza at gmail.com
//
//    This library is free software; you can redistribute it and/or
//    modify it under the terms of the GNU Lesser General Public
//    License as published by the Free Software Foundation; either
//    version 2.1 of the License, or (at your option) any later version.
//
//    This library is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//    Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public
//    License along with this library; if not, write to the Free Software
//    Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
//

namespace Accord.Statistics.Distributions.Univariate
{
    using System;
    using Accord.Math;
    using Accord.Statistics.Distributions;
    using Accord.Statistics.Distributions.Fitting;
    using Accord.Statistics.Distributions.Multivariate;
    using AForge;

    /// <summary>
    ///   Log-Logistic distribution.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    ///   In probability and statistics, the log-logistic distribution (known as the Fisk 
    ///   distribution in economics) is a continuous probability distribution for a non-negative
    ///   random variable. It is used in survival analysis as a parametric model for events 
    ///   whose rate increases initially and decreases later, for example mortality rate from
    ///   cancer following diagnosis or treatment. It has also been used in hydrology to model 
    ///   stream flow and precipitation, and in economics as a simple model of the distribution
    ///   of wealth or income.</para>
    ///   
    /// <para>
    ///   The log-logistic distribution is the probability distribution of a random variable
    ///   whose logarithm has a logistic distribution. It is similar in shape to the log-normal
    ///   distribution but has heavier tails. Its cumulative distribution function can be written
    ///   in closed form, unlike that of the log-normal.</para>
    ///   
    /// <para>
    ///   References:
    ///   <list type="bullet">
    ///     <item><description><a href="http://en.wikipedia.org/wiki/Log-logistic_distribution">
    ///       Wikipedia, The Free Encyclopedia. Log-logistic distribution. Available on: 
    ///       http://en.wikipedia.org/wiki/Log-logistic_distribution </a></description></item>
    ///   </list></para> 
    /// </remarks>
    /// 
    /// <example>
    /// <para>
    ///   This examples shows how to create a Log-Logistic distribution
    ///   and compute some of its properties and characteristics.</para>
    ///   
    /// <code>
    /// // Create a LLD2 distribution with scale = 3, shape = 0.5

    /// </code>
    /// </example>
    /// 
    /// <seealso cref="LogisticDistribution"/>
    /// <seealso cref="ShiftedLogLogisticDistribution"/>
    /// 
    [Serializable]
    public class LogLogisticDistribution : UnivariateContinuousDistribution
    {

        // Distribution parameters
        private double alpha;   // scale α
        private double beta;    // shape β

        /// <summary>
        ///   Constructs a Log-Logistic distribution
        ///   with unit scale and unit shape.
        /// </summary>
        /// 
        public LogLogisticDistribution()
        {
            initialize(0, 1);
        }

        /// <summary>
        ///   Constructs a Log-Logistic distribution
        ///   with the given scale and unit shape.
        /// </summary>
        /// 
        /// <param name="scale">The distribution's scale value α (alpha).</param>
        /// 
        public LogLogisticDistribution(double scale)
        {
            if (scale <= 0)
                throw new ArgumentOutOfRangeException("scale", "Scale must be positive.");

            initialize(scale, 1);
        }

        /// <summary>
        ///   Constructs a Log-Logistic distribution
        ///   with the given scale and shape parameters.
        /// </summary>
        /// 
        /// <param name="scale">The distribution's scale value α (alpha).</param>
        /// <param name="shape">The distribution's shape value β (beta).</param>
        /// 
        public LogLogisticDistribution(double scale, double shape)
        {
            if (scale <= 0)
                throw new ArgumentOutOfRangeException("scale", "Scale must be positive.");

            if (shape <= 0)
                throw new ArgumentOutOfRangeException("scale", "Scale must be positive.");

            initialize(scale, shape);
        }


        /// <summary>
        ///   Gets the distribution's scale value (α).
        /// </summary>
        /// 
        public double Scale
        {
            get { return alpha; }
        }

        /// <summary>
        ///   Gets the distribution's shape value (β).
        /// </summary>
        /// 
        public double Shape
        {
            get { return beta; }
        }

        /// <summary>
        ///   Gets the mean for this distribution.
        /// </summary>
        /// 
        /// <value>
        ///   The distribution's mean value.
        /// </value>
        /// 
        public override double Mean
        {
            get
            {
                if (beta <= 1)
                    return Double.NaN;

                double b = Math.PI / beta;
                return (alpha * b) / Math.Sin(b);
            }
        }

        /// <summary>
        ///   Gets the median for this distribution.
        /// </summary>
        /// 
        /// <value>
        ///   The distribution's median value.
        /// </value>
        /// 
        public override double Median
        {
            get
            {
                System.Diagnostics.Debug.Assert(alpha == base.Median);
                return alpha;
            }
        }

        /// <summary>
        ///   Gets the variance for this distribution.
        /// </summary>
        /// 
        /// <value>
        ///   The distribution's variance.
        /// </value>
        /// 
        public override double Variance
        {
            get
            {
                if (beta <= 2)
                    return Double.NaN;

                double b = Math.PI / beta;
                double c = b * Math.Sin(b);
                return alpha * alpha * (2 * b / Math.Sin(2 * b) - c * c);
            }
        }

        /// <summary>
        ///   Gets the mode for this distribution.
        /// </summary>
        /// 
        /// <remarks>
        ///   In the logistic distribution, the mode is equal
        ///   to the distribution <see cref="Mean"/> value.
        /// </remarks>
        /// 
        /// <value>
        ///   The distribution's mode value.
        /// </value>
        /// 
        public override double Mode
        {
            get
            {
                if (beta <= 1)
                    return Double.NaN;
                return alpha * Math.Pow((beta - 1) / (beta + 1), 1 / beta);
            }
        }

        /// <summary>
        ///   Not supported.
        /// </summary>
        /// 
        public override double Entropy
        {
            get { throw new NotSupportedException(); }
        }

        /// <summary>
        ///   Gets the support interval for this distribution.
        /// </summary>
        /// 
        /// <value>
        ///   A <see cref="AForge.DoubleRange" /> containing
        ///   the support interval for this distribution.
        /// </value>
        /// 
        public override DoubleRange Support
        {
            get { return new DoubleRange(0, Double.PositiveInfinity); }
        }

        /// <summary>
        ///   Gets the cumulative distribution function (cdf) for
        ///   this distribution evaluated at point <c>x</c>.
        /// </summary>
        /// 
        /// <param name="x">A single point in the distribution range.</param>
        /// 
        public override double DistributionFunction(double x)
        {
            double xb = Math.Pow(x, beta);
            double ab = Math.Pow(alpha, beta);

            return xb / (ab + xb);
        }

        /// <summary>
        ///   Gets the probability density function (pdf) for
        ///   this distribution evaluated at point <c>x</c>.
        /// </summary>
        /// 
        /// <param name="x">A single point in the distribution range.</param>
        /// 
        /// <returns>
        ///   The probability of <c>x</c> occurring
        ///   in the current distribution.
        /// </returns>
        /// 
        public override double ProbabilityDensityFunction(double x)
        {
            double ba = beta / alpha;
            double xa = x / alpha;

            double num = ba * Math.Pow(xa, beta - 1);
            double den = 1 + Math.Pow(xa, beta);

            return num / (den * den);
        }

        /// <summary>
        ///   Gets the inverse of the cumulative distribution function (icdf) for
        ///   this distribution evaluated at probability <c>p</c>. This function
        ///   is also known as the Quantile function.
        /// </summary>
        /// 
        /// <param name="p">A probability value between 0 and 1.</param>
        /// 
        /// <returns>
        ///   A sample which could original the given probability
        ///   value when applied in the <see cref="DistributionFunction"/>.
        /// </returns>
        /// 
        public override double InverseDistributionFunction(double p)
        {
            return alpha * Math.Pow(p / (1 - p), 1 / beta);
        }

        /// <summary>
        ///   Gets the first derivative of the <see cref="InverseDistributionFunction">
        ///   inverse distribution function</see> (icdf) for this distribution evaluated
        ///   at probability <c>p</c>. 
        /// </summary>
        /// 
        /// <param name="p">A probability value between 0 and 1.</param>
        /// 
        public override double QuantileDensityFunction(double p)
        {
            double qdf = (alpha / beta) * Math.Pow(p / (1 - p), 1 / beta - 1);

            System.Diagnostics.Debug.Assert(qdf == base.QuantileDensityFunction(p));

            return qdf;
        }

        /// <summary>
        ///   Gets the complementary cumulative distribution function
        ///   (ccdf) for this distribution evaluated at point <c>x</c>.
        ///   This function is also known as the Survival function.
        /// </summary>
        /// 
        /// <param name="x">A single point in the distribution range.</param>
        /// 
        /// <remarks>
        ///   The Complementary Cumulative Distribution Function (CCDF) is
        ///   the complement of the Cumulative Distribution Function, or 1
        ///   minus the CDF.
        /// </remarks>
        /// 
        public override double ComplementaryDistributionFunction(double x)
        {
            double xa = x / alpha;
            double den = 1 + Math.Pow(xa, beta);

            double icdf = 1 / den;

            System.Diagnostics.Debug.Assert(icdf == base.ComplementaryDistributionFunction(x));

            return icdf;
        }

        /// <summary>
        ///   Gets the hazard function, also known as the failure rate or
        ///   the conditional failure density function for this distribution
        ///   evaluated at point <c>x</c>.
        /// </summary>
        /// 
        /// <param name="x">A single point in the distribution range.</param>
        /// 
        /// <returns>
        ///   The conditional failure density function <c>h(x)</c>
        ///   evaluated at <c>x</c> in the current distribution.
        /// </returns>
        /// 
        /// <remarks>
        ///   The hazard function is the ratio of the probability
        ///   density function f(x) to the survival function, S(x).
        /// </remarks>
        /// 
        public override double HazardFunction(double x)
        {
            double ba = beta / alpha;
            double xa = x / alpha;

            double num = ba * Math.Pow(xa, beta - 1);
            double den = 1 + Math.Pow(xa, beta);

            double h = num / den;

            System.Diagnostics.Debug.Assert(h == base.HazardFunction(x));

            return h;
        }

        /// <summary>
        ///   Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        ///   A new object that is a copy of this instance.
        /// </returns>
        /// 
        public override object Clone()
        {
            return new LogLogisticDistribution(alpha, beta);
        }

        /// <summary>
        ///   Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// 
        /// <returns>
        ///   A <see cref="System.String"/> that represents this instance.
        /// </returns>
        /// 
        public override string ToString()
        {
            return String.Format("LogLogistic(x; α = {0}, β = {1})", alpha, beta);
        }

        /// <summary>
        ///   Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// 
        /// <returns>
        ///   A <see cref="System.String"/> that represents this instance.
        /// </returns>
        /// 
        public string ToString(IFormatProvider formatProvider)
        {
            return String.Format(formatProvider, "LogLogistic(x; α = {0}, β = {1})", alpha, beta);
        }

        /// <summary>
        ///   Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// 
        /// <returns>
        ///   A <see cref="System.String"/> that represents this instance.
        /// </returns>
        /// 
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return String.Format(formatProvider, "LogLogistic(x; α = {0}, β = {1})",
                alpha.ToString(format, formatProvider),
                beta.ToString(format, formatProvider));
        }

        /// <summary>
        ///   Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// 
        /// <returns>
        ///   A <see cref="System.String"/> that represents this instance.
        /// </returns>
        /// 
        public string ToString(string format)
        {
            return String.Format("LogLogistic(x; α = {0}, β = {1})",
                alpha.ToString(format), beta.ToString(format));
        }


        private void initialize(double scale, double shape)
        {
            this.alpha = scale;
            this.beta = shape;
        }
    }
}
