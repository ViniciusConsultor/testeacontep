using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Globalization;

namespace Acontep.Manutencao.Seguranca
{
    internal static class ConversaoValor
    {
        internal static string ObterDefaultAppName()
        {
            try
            {
                string appName = System.Web.HttpRuntime.AppDomainAppVirtualPath;
                if (appName == null || appName.Length == 0)
                {
                    return "/";
                }
                else
                {
                    return appName;
                }
            }
            catch
            {
                return "/";
            }
        }

        internal static bool ValidarParametro(ref string param, int maxSize)
        {
            if (param == null)
            {
                return false;
            }

            if (param.Trim().Length < 1)
            {
                return false;
            }

            if (maxSize > 0 && param.Length > maxSize)
            {
                return false;
            }

            return true;
        }


        internal static bool ValidarParametro(ref string param, bool checkForNull, bool checkIfEmpty, bool checkForCommas, int maxSize)
        {
            if (param == null)
            {
                if (checkForNull)
                {
                    return false;
                }

                return true;
            }

            param = param.Trim();
            if ((checkIfEmpty && param.Length < 1) ||
                 (maxSize > 0 && param.Length > maxSize) ||
                 (checkForCommas && param.IndexOf(",") != -1))
            {
                return false;
            }

            return true;
        }

        internal static void ChecarParametro(ref string param, int maxSize, string paramName)
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (param.Trim().Length < 1)
            {
                throw new ArgumentException("The parameter '" + paramName + "' must not be empty.",
                    paramName);
            }

            if (maxSize > 0 && param.Length > maxSize)
            {
                throw new ArgumentException("The parameter '" + paramName + "' is too long: it must not exceed " + maxSize.ToString(CultureInfo.InvariantCulture) + " chars in length.",
                    paramName);
            }
        }

        internal static void ChecarParametro(ref string param, bool checkForNull, bool checkIfEmpty, bool checkForCommas, int maxSize, string paramName)
        {
            if (param == null)
            {
                if (checkForNull)
                {
                    throw new ArgumentNullException(paramName);
                }

                return;
            }

            param = param.Trim();

            if (checkIfEmpty && param.Length < 1)
            {
                throw new ArgumentException("The parameter '" + paramName + "' must not be empty.",
                    paramName);
            }

            if (maxSize > 0 && param.Length > maxSize)
            {
                throw new ArgumentException("The parameter '" + paramName + "' is too long: it must not exceed " + maxSize.ToString(CultureInfo.InvariantCulture) + " chars in length.",
                    paramName);
            }

            if (checkForCommas && param.IndexOf(',') != -1)
            {
                throw new ArgumentException("The parameter '" + paramName + "' must not contain commas.",
                    paramName);
            }
        }

        internal static void ChecarArrayParametro(ref string[] param, bool checkForNull, bool checkIfEmpty, bool checkForCommas, int maxSize, string paramName)
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (param.Length < 1)
            {
                throw new ArgumentException("The array parameter '" + paramName + "' should not be empty.", paramName);
            }

            for (int i = param.Length - 1; i >= 0; i--)
            {

                ConversaoValor.ChecarParametro(ref param[i],
                                           checkForNull,
                                           checkIfEmpty,
                                           checkForCommas,
                                           maxSize,
                                           paramName + "[ " + i.ToString(CultureInfo.InvariantCulture) + " ]");
            }

            for (int i = param.Length - 1; i >= 0; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    if (param[i].Equals(param[j]))
                    {
                        throw new ArgumentException("The array '" + paramName + "' should not contain duplicate values.",
                            paramName);
                    }
                }
            }
        }

        internal static bool ObterValorBoolean(NameValueCollection config, string valueName, bool defaultValue)
        {
            string sValue = config[valueName];
            if (sValue == null)
            {
                return defaultValue;
            }

            if (sValue == "true")
            {
                return true;
            }

            if (sValue == "false")
            {
                return false;
            }

            throw new Exception("The value must be a boolean for property '" + valueName + "'");
        }

        internal static int ObterValorInt32(NameValueCollection config, string valueName, int defaultValue, bool zeroAllowed, int maxValueAllowed)
        {
            string sValue = config[valueName];

            if (sValue == null)
            {
                return defaultValue;
            }

            int iValue;
            try
            {
                iValue = Convert.ToInt32(sValue, CultureInfo.InvariantCulture);
            }
            catch (InvalidCastException e)
            {
                if (zeroAllowed)
                {
                    throw new Exception("The value must be a positive integer for property '" + valueName + "'", e);
                }

                throw new Exception("The value must be a positive integer for property '" + valueName + "'", e);
            }

            if (zeroAllowed && iValue < 0)
            {
                throw new Exception("The value must be a non-negative integer for property '" + valueName + "'");
            }

            if (!zeroAllowed && iValue <= 0)
            {
                throw new Exception("The value must be a non-negative integer for property '" + valueName + "'");
            }

            if (maxValueAllowed > 0 && iValue > maxValueAllowed)
            {
                throw new Exception("The value is too big for '" + valueName + "' must be smaller than " + maxValueAllowed.ToString(CultureInfo.InvariantCulture));
            }

            return iValue;
        }
    }
}
