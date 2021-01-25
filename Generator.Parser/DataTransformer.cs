using System;
using System.Collections.Generic;
using System.Globalization;

namespace Generator.Parser
{
    public static class DataTransformer
    {
        public static string TrimSpaces(string value, object parameters, object instance, object mainInstance)
        {
            return value.Trim();
        }

        public static string NotNull(string value, object parameters, object instance, object mainInstance)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException($"El valor es obligatorio, no se ha encontrado valor configurado");
            }
            return value;
        }

        public static string Split(string value, object parameters, object instance, object mainInstance)
        {
            try
            {
                if (parameters == null)
                {
                    throw new NullReferenceException("No hay parametros configurados");
                }
                var dicParams = (IDictionary<string, object>)parameters;
                //var listDic = dicParams.Values.ToList();
                if (dicParams.Count < 2)
                {
                    throw new ArgumentException("No existen suficientes parametros para ejecutar la función");
                }
                //return value.Split(listDic[0])[Convert.ToInt32(listDic[1])];
                return value.Split(dicParams["splitCharacter"].ToString())[Convert.ToInt32(dicParams["splitPosition"].ToString())];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string DateFormat(string value, object parameters, object instance, object mainInstance)
        {
            try
            {
                if (parameters == null)
                {
                    throw new NullReferenceException("No hay parametros configurados");
                }
                var dicParams = (IDictionary<string, object>)parameters;
                //var listDic = dicParams.Values.ToList();
                if (dicParams.Count < 2)
                {
                    throw new ArgumentException("No existen suficientes parametros para ejecutar la función");
                }
                //return DateTime.ParseExact(value, listDic[0], CultureInfo.InvariantCulture).ToString(listDic[1]);
                return DateTime.ParseExact(value, dicParams["inputDateFormat"].ToString(), CultureInfo.InvariantCulture)
                        .ToString(dicParams["outputDateFormat"].ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string NumericalFormat(string value, object parameters, object instance, object mainInstance)
        {
            try
            {
                if (parameters == null)
                {
                    throw new NullReferenceException("No hay parametros configurados");
                }
                var dicParams = (IDictionary<string, object>)parameters;
                //var listDic = dicParams.Values.ToList();
                if (dicParams.Count < 2)
                {
                    throw new ArgumentException("No existen suficientes parametros para ejecutar la función");
                }
                /* int decimalCount = Convert.ToInt32(listDic[0]);
                 * CultureInfo cultureInfo = new CultureInfo(listDic[1]);
                 */
                int decimalCount = Convert.ToInt32(dicParams["decimalCount"].ToString());
                CultureInfo cultureInfo = new CultureInfo(dicParams["culture"].ToString());
                if (value.Equals("0"))
                {
                    var complete = "D" + (decimalCount + 1).ToString();
                    value = int.Parse(value).ToString(complete);
                }
                else if (value.Contains("-"))
                {
                    value = "-" + value.Split('-')[1];
                }

                double _value = Convert.ToDouble(value.Substring(0, value.Length - decimalCount) + "," + value.Substring(value.Length - decimalCount));
                return _value.ToString("N", cultureInfo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string GetValue(string value, object parameters, object instance, object mainInstance)
        {
            try
            {
                if (parameters == null)
                {
                    throw new NullReferenceException("No hay parametros configurados");
                }
                var dicParams = (IDictionary<string, object>)parameters;
                //var listDic = dicParams.Values.ToList();
                if (dicParams.Count < 1)
                {
                    throw new ArgumentException("No existen suficientes parametros para ejecutar la función");
                }
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static object GetValue(object instance, string propName)
        {
            return instance.GetType().GetProperty(propName).GetValue(instance, null);
        }

        public static string SetValue(string value, object parameters, object instance, object mainInstance)
        {
            try
            {
                if (parameters == null)
                {
                    throw new NullReferenceException("No hay parametros configurados");
                }
                var dicParams = (IDictionary<string, object>)parameters;
                //var listDic = dicParams.Values.ToList();
                if (dicParams.Count < 1)
                {
                    throw new ArgumentException("No existen suficientes parametros para ejecutar la función");
                }
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string HideString(string value, object parameters, object instance, object mainInstance)
        {
            try
            {
                if (parameters == null)
                {
                    throw new NullReferenceException("No hay parametros configurados");
                }
                var dicParams = (IDictionary<string, object>)parameters;
                //var listDic = dicParams.Values.ToList();
                if (dicParams.Count < 3)
                {
                    throw new ArgumentException("No existen suficientes parametros para ejecutar la función");
                }
                var characters = value.ToCharArray();
                var hideText = "";
                var hideFinal = Convert.ToBoolean(dicParams["hideFinal"].ToString());
                int lengthUnhide = Convert.ToInt32(dicParams["lengthUnhide"].ToString());
                var charToReplace = dicParams["charToReplace"].ToString();
                for (int i = 0; i < value.Length; i++)
                {
                    if (hideFinal)
                    {
                        hideText += (i >= lengthUnhide) ? charToReplace : characters[i].ToString();
                    }
                    else
                    {
                        hideText += (i < value.Length - lengthUnhide) ? charToReplace : characters[i].ToString();
                    }

                }
                return hideText;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string SubstringValues(string value, object parameters, object instance, object mainInstance)
        {
            try
            {
                if (parameters == null)
                {
                    throw new NullReferenceException("No hay parametros configurados");
                }
                var dicParams = (IDictionary<string, object>)parameters;
                //var listDic = dicParams.Values.ToList();
                if (dicParams.Count < 2)
                {
                    throw new ArgumentException("No existen suficientes parametros para ejecutar la función");
                }
                int initialPosition = Convert.ToInt32(dicParams["initialPosition"].ToString());
                int quantityChars = Convert.ToInt32(dicParams["quantityCharacters"].ToString());
                if (initialPosition+quantityChars > value.Length)
                {
                    return "";
                    //throw new ArgumentOutOfRangeException("No es posible realizar el substring, los valores sobrepasan el valor");
                }
                return value.Substring(initialPosition,quantityChars);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string GetEquivalentRule(string value, object parameters, object instance, object mainInstance)
        {
            try
            {
                if (parameters == null)
                {
                    throw new NullReferenceException("No hay parametros configurados");
                }
                var dicParams = (IDictionary<string, object>)parameters;
                //var listDic = dicParams.Values.ToList();
                if (dicParams.Count < 2)
                {
                    throw new ArgumentException("No existen suficientes parametros para ejecutar la función");
                }
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string AddChannel(string value, object parameters, object instance, object mainInstance)
        {
            try
            {
                if (parameters == null)
                {
                    throw new NullReferenceException("No hay parametros configurados");
                }
                var dicParams = (IDictionary<string, object>)parameters;
                //var listDic = dicParams.Values.ToList();

                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string Replace(string value, object parameters, object instance, object mainInstance)
        {
            try
            {
                if (parameters == null)
                {
                    throw new NullReferenceException("No hay parametros configurados");
                }
                var dicParams = (IDictionary<string, object>)parameters;
                //var listDic = dicParams.Values.ToList();
                
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string ReplaceChar(string value, object parameters, object instance, object mainInstance)
        {
            try
            {
                if (parameters == null)
                {
                    throw new NullReferenceException("No hay parametros configurados");
                }
                var dicParams = (IDictionary<string, object>)parameters;
                //var listDic = dicParams.Values.ToList();
                if (dicParams.Count < 2)
                {
                    throw new ArgumentException("No existen suficientes parametros para ejecutar la función");
                }
                return value.Replace(dicParams["charToReplace"].ToString(),dicParams["newChar"].ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string ReplaceEmpty(string value, object parameters, object instance, object mainInstance)
        {
            try
            {
                if (parameters == null)
                {
                    throw new NullReferenceException("No hay parametros configurados");
                }
                var dicParams = (IDictionary<string, object>)parameters;
                //var listDic = dicParams.Values.ToList();
                if (dicParams.Count < 1)
                {
                    throw new ArgumentException("No existen suficientes parametros para ejecutar la función");
                }
                return (string.IsNullOrWhiteSpace(value)) ? dicParams["value"].ToString() : value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static string GetResource(string value, object parameters, object instance, object mainInstance)
        {
            try
            {
                if (parameters == null)
                {
                    throw new NullReferenceException("No hay parametros configurados");
                }
                var dicParams = (IDictionary<string, object>)parameters;
                //var listDic = dicParams.Values.ToList();

                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string ConcatenatePropValues(string value, object parameters, object instance, object mainInstance)
        {
            try
            {
                if (parameters == null)
                {
                    throw new NullReferenceException("No hay parametros configurados");
                }
                var dicParams = (IDictionary<string, object>)parameters;
                //var listDic = dicParams.Values.ToList();
                if (dicParams.Count < 2)
                {
                    throw new ArgumentException("No existen suficientes parametros para ejecutar la función");
                }
                
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string SubstractDates(string value, object parameters, object instance, object mainInstance)
        {
            try
            {
                if (parameters == null)
                {
                    throw new NullReferenceException("No hay parametros configurados");
                }
                var dicParams = (IDictionary<string, object>)parameters;
                //var listDic = dicParams.Values.ToList();
                if (dicParams.Count < 2)
                {
                    throw new ArgumentException("No existen suficientes parametros para ejecutar la función");
                }

                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string GreaterEqual(string value, object parameters, object instance, object mainInstance)
        {
            try
            {
                if (parameters == null)
                {
                    throw new NullReferenceException("No hay parametros configurados");
                }
                var dicParams = (IDictionary<string, object>)parameters;
                //var listDic = dicParams.Values.ToList();
                if (dicParams.Count < 2)
                {
                    throw new ArgumentException("No existen suficientes parametros para ejecutar la función");
                }

                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string GreaterThan(string value, object parameters, object instance, object mainInstance)
        {
            try
            {
                if (parameters == null)
                {
                    throw new NullReferenceException("No hay parametros configurados");
                }
                var dicParams = (IDictionary<string, object>)parameters;
                //var listDic = dicParams.Values.ToList();
                if (dicParams.Count < 2)
                {
                    throw new ArgumentException("No existen suficientes parametros para ejecutar la función");
                }

                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string LessEqual(string value, object parameters, object instance, object mainInstance)
        {
            try
            {
                if (parameters == null)
                {
                    throw new NullReferenceException("No hay parametros configurados");
                }
                var dicParams = (IDictionary<string, object>)parameters;
                //var listDic = dicParams.Values.ToList();
                if (dicParams.Count < 2)
                {
                    throw new ArgumentException("No existen suficientes parametros para ejecutar la función");
                }

                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string LessThan(string value, object parameters, object instance, object mainInstance)
        {
            try
            {
                if (parameters == null)
                {
                    throw new NullReferenceException("No hay parametros configurados");
                }
                var dicParams = (IDictionary<string, object>)parameters;
                //var listDic = dicParams.Values.ToList();
                if (dicParams.Count < 2)
                {
                    throw new ArgumentException("No existen suficientes parametros para ejecutar la función");
                }

                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string GetPayDate(string value, object parameters, object instance, object mainInstance)
        {
            try
            {
                if (parameters == null)
                {
                    throw new NullReferenceException("No hay parametros configurados");
                }                

                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
