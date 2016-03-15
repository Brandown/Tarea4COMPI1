
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;
using System.Linq;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message)
            : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner)
            : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message)
            : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner)
            : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context)
            : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF = 0, // (EOF)
        SYMBOL_ERROR = 1, // (Error)
        SYMBOL_WHITESPACE = 2, // (Whitespace)
        SYMBOL_COMMENTLINE = 3, // (Comment Line)
        SYMBOL_COMMENTSTART = 4, // (Comment Start)
        SYMBOL_AMP = 5, // '&'
        SYMBOL_COLON = 6, // ':'
        SYMBOL_SEMI = 7, // ';'
        SYMBOL_AMARILLO = 8, // amarillo
        SYMBOL_CIRCULO = 9, // circulo
        SYMBOL_CUADRADO = 10, // cuadrado
        SYMBOL_FIGURA = 11, // Figura
        SYMBOL_GRANDE = 12, // grande
        SYMBOL_LETRA = 13, // letra
        SYMBOL_MEDIANO = 14, // mediano
        SYMBOL_NUMERO = 15, // numero
        SYMBOL_RECTANGULO = 16, // rectangulo
        SYMBOL_ROJO = 17, // rojo
        SYMBOL_TILDE = 18, // tilde
        SYMBOL_TITULO = 19, // Titulo
        SYMBOL_VERDE = 20, // verde
        SYMBOL_COLOR = 21, // <color>
        SYMBOL_FIGURA2 = 22, // <figura>
        SYMBOL_INICIO = 23, // <inicio>
        SYMBOL_PALABRA = 24, // <palabra>
        SYMBOL_SIZE = 25, // <size>
        SYMBOL_TIPO = 26  // <tipo>
    };

    enum RuleConstants : int
    {
        RULE_INICIO = 0, // <inicio> ::= <figura>
        RULE_INICIO2 = 1, // <inicio> ::= <inicio> <figura>
        RULE_FIGURA_FIGURA_COLON_COLON_AMP_SEMI = 2, // <figura> ::= Figura ':' ':' <tipo> '&' <color> ';'
        RULE_FIGURA_TITULO_COLON_COLON_AMP_AMP_SEMI = 3, // <figura> ::= Titulo ':' ':' <palabra> '&' <color> '&' <size> ';'
        RULE_PALABRA_LETRA = 4, // <palabra> ::= letra
        RULE_PALABRA_NUMERO = 5, // <palabra> ::= numero
        RULE_PALABRA_TILDE = 6, // <palabra> ::= tilde
        RULE_PALABRA_LETRA2 = 7, // <palabra> ::= <palabra> letra
        RULE_PALABRA_NUMERO2 = 8, // <palabra> ::= <palabra> numero
        RULE_PALABRA_TILDE2 = 9, // <palabra> ::= <palabra> tilde
        RULE_TIPO_CIRCULO = 10, // <tipo> ::= circulo
        RULE_TIPO_CUADRADO = 11, // <tipo> ::= cuadrado
        RULE_TIPO_RECTANGULO = 12, // <tipo> ::= rectangulo
        RULE_COLOR_ROJO = 13, // <color> ::= rojo
        RULE_COLOR_VERDE = 14, // <color> ::= verde
        RULE_COLOR_AMARILLO = 15, // <color> ::= amarillo
        RULE_SIZE_MEDIANO = 16, // <size> ::= mediano
        RULE_SIZE_GRANDE = 17  // <size> ::= grande
    };

    public class MyParser
    {
        private LALRParser parser;
        //instanciamos un unico objeto el cual contendra lista y algunas otras variables ver referenca clase



        public MyParser(string filename)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open,
                                               FileAccess.Read,
                                               FileShare.Read);
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnReduce += new LALRParser.ReduceHandler(ReduceEvent);
            parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenReadEvent);
            parser.OnAccept += new LALRParser.AcceptHandler(AcceptEvent);
            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
        }

        public void Parse(string source)
        {
            parser.Parse(source);

        }

        private void TokenReadEvent(LALRParser parser, TokenReadEventArgs args)
        {
            try
            {
                args.Token.UserObject = CreateObject(args.Token);
            }
            catch (Exception e)
            {
                args.Continue = true;
                //todo: Report message to UI?
            }
        }

        private Object CreateObject(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF:
                    //(EOF)
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_ERROR:
                    //(Error)
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE:
                    //(Whitespace)
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_COMMENTLINE:
                    //(Comment Line)
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_COMMENTSTART:
                    //(Comment Start)
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_AMP:
                    //'&'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_COLON:
                    //':'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_SEMI:
                    //';'
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_AMARILLO:
                    //amarillo
                    //todo: Create a new object that corresponds to the symbol
                    return token.Text;

                case (int)SymbolConstants.SYMBOL_CIRCULO:
                    //circulo
                    //todo: Create a new object that corresponds to the symbol
                    return token.Text;

                case (int)SymbolConstants.SYMBOL_CUADRADO:
                    //cuadrado
                    //todo: Create a new object that corresponds to the symbol
                    return token.Text;

                case (int)SymbolConstants.SYMBOL_FIGURA:
                    //Figura
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_GRANDE:
                    //grande
                    //todo: Create a new object that corresponds to the symbol
                    return token.Text;

                case (int)SymbolConstants.SYMBOL_LETRA:
                    //letra
                    //todo: Create a new object that corresponds to the symbol
                    return token.Text;

                case (int)SymbolConstants.SYMBOL_MEDIANO:
                    //mediano
                    //todo: Create a new object that corresponds to the symbol
                    return token.Text;

                case (int)SymbolConstants.SYMBOL_NUMERO:
                    //numero
                    //todo: Create a new object that corresponds to the symbol
                    return token.Text;

                case (int)SymbolConstants.SYMBOL_RECTANGULO:
                    //rectangulo
                    //todo: Create a new object that corresponds to the symbol
                    return token.Text;

                case (int)SymbolConstants.SYMBOL_ROJO:
                    //rojo
                    //todo: Create a new object that corresponds to the symbol
                    return token.Text;

                case (int)SymbolConstants.SYMBOL_TILDE:
                    //tilde
                    //todo: Create a new object that corresponds to the symbol
                    return token.Text;

                case (int)SymbolConstants.SYMBOL_TITULO:
                    //Titulo
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_VERDE:
                    //verde
                    //todo: Create a new object that corresponds to the symbol
                    return token.Text;

                case (int)SymbolConstants.SYMBOL_COLOR:
                    //<color>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_FIGURA2:
                    //<figura>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_INICIO:
                    //<inicio>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_PALABRA:
                    //<palabra>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_SIZE:
                    //<size>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

                case (int)SymbolConstants.SYMBOL_TIPO:
                    //<tipo>
                    //todo: Create a new object that corresponds to the symbol
                    return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        private void ReduceEvent(LALRParser parser, ReduceEventArgs args)
        {
            try
            {
                args.Token.UserObject = CreateObject(args.Token);
            }
            catch (Exception e)
            {//lo modificamos en true para que continue de todas formas
                args.Continue = true;
                //todo: Report message to UI?
            }
        }

        public static Object CreateObject(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_INICIO:
                    //<inicio> ::= <figura>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_INICIO2:
                    //<inicio> ::= <inicio> <figura>
                    //todo: Create a new object using the stored user objects.
                    return null;

                case (int)RuleConstants.RULE_FIGURA_FIGURA_COLON_COLON_AMP_SEMI:
                    //<figura> ::= Figura ':' ':' <tipo> '&' <color> ';'
                    //todo: Create a new object using the stored user objects.
                    {
                        Dibujo.nodo_figura nuevo = new Dibujo.nodo_figura();
                        //llenamos el nodo
                        nuevo.insertaTipo(token.Tokens[3].UserObject.ToString());
                        nuevo.insertaColor(token.Tokens[5].UserObject.ToString());
                        insertarFigura_lista(nuevo);
                        return null;
                    }
                case (int)RuleConstants.RULE_FIGURA_TITULO_COLON_COLON_AMP_AMP_SEMI:
                    //<figura> ::= Titulo ':' ':' <palabra> '&' <color> '&' <size> ';'
                    //todo: Create a new object using the stored user objects.
                    {
                        Dibujo.nodo_figura nuevo = new Dibujo.nodo_figura();
                        //llenamos el nodo
                        nuevo.insertaTipo("Titulo");
                        nuevo.insertaTexto(token.Tokens[3].UserObject.ToString());
                        nuevo.insertaColor(token.Tokens[5].UserObject.ToString());
                        nuevo.insertaTamano(token.Tokens[7].UserObject.ToString());
                        insertarFigura_lista(nuevo);
                        return null;
                    }
                case (int)RuleConstants.RULE_PALABRA_LETRA:
                    //<palabra> ::= letra
                    //todo: Create a new object using the stored user objects.
                    return token.Tokens[0].UserObject.ToString();

                case (int)RuleConstants.RULE_PALABRA_NUMERO:
                    //<palabra> ::= numero
                    //todo: Create a new object using the stored user objects.
                    return token.Tokens[0].UserObject.ToString();

                case (int)RuleConstants.RULE_PALABRA_TILDE:
                    //<palabra> ::= tilde
                    //todo: Create a new object using the stored user objects.
                    return token.Tokens[0].UserObject;

                case (int)RuleConstants.RULE_PALABRA_LETRA2:
                    //<palabra> ::= <palabra> letra
                    //todo: Create a new object using the stored user objects.
                    {
                        /*importante mensionar que como el parser retorna objetos para concatenar dos objetos los pasamos 
                         * a String*/
                        return token.Tokens[0].UserObject.ToString() + " " + token.Tokens[1].UserObject.ToString();//importante el espacion sino el texto saldra junto
                    }
                case (int)RuleConstants.RULE_PALABRA_NUMERO2:
                    //<palabra> ::= <palabra> numero
                    //todo: Create a new object using the stored user objects.
                    return token.Tokens[0].UserObject.ToString() + " " + token.Tokens[1].UserObject.ToString();//importante el espacion sino el texto saldra junto

                case (int)RuleConstants.RULE_PALABRA_TILDE2:
                    //<palabra> ::= <palabra> tilde
                    //todo: Create a new object using the stored user objects.
                    return token.Tokens[0].UserObject.ToString() + " " + token.Tokens[1].UserObject.ToString();//importante el espacion sino el texto saldra junto

                case (int)RuleConstants.RULE_TIPO_CIRCULO:
                    //<tipo> ::= circulo
                    //todo: Create a new object using the stored user objects.
                    return token.Tokens[0].UserObject;

                case (int)RuleConstants.RULE_TIPO_CUADRADO:
                    //<tipo> ::= cuadrado
                    //todo: Create a new object using the stored user objects.
                    return token.Tokens[0].UserObject.ToString();

                case (int)RuleConstants.RULE_TIPO_RECTANGULO:
                    //<tipo> ::= rectangulo
                    //todo: Create a new object using the stored user objects.
                    return token.Tokens[0].UserObject.ToString();

                case (int)RuleConstants.RULE_COLOR_ROJO:
                    //<color> ::= rojo
                    //todo: Create a new object using the stored user objects.
                    return token.Tokens[0].UserObject.ToString();

                case (int)RuleConstants.RULE_COLOR_VERDE:
                    //<color> ::= verde
                    //todo: Create a new object using the stored user objects.
                    return token.Tokens[0].UserObject.ToString();

                case (int)RuleConstants.RULE_COLOR_AMARILLO:
                    //<color> ::= amarillo
                    //todo: Create a new object using the stored user objects.
                    return token.Tokens[0].UserObject.ToString();

                case (int)RuleConstants.RULE_SIZE_MEDIANO:
                    //<size> ::= mediano
                    //todo: Create a new object using the stored user objects.
                    return token.Tokens[0].UserObject.ToString();

                case (int)RuleConstants.RULE_SIZE_GRANDE:
                    //<size> ::= grande
                    //todo: Create a new object using the stored user objects.
                    return token.Tokens[0].UserObject.ToString();

            }
            throw new RuleException("Unknown rule");
        }

        private static void insertarFigura_lista(Dibujo.nodo_figura nuevo)
        {

            Dibujo.Mi_Clase.milista.AddLast(nuevo);
        }





        private void AcceptEvent(LALRParser parser, AcceptEventArgs args)
        {
            //todo: Use your fully reduced args.Token.UserObject
            //evento para aceptar mostrara el mensaje final
            MessageBox.Show("Finalizo de analizar el archivo.");
        }


        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            //  string message = "Token error with input: '"+args.Token.ToString()+"'";
            //  args.Continue = true;
            //todo: Report message to UI?

            /*como muchas veces un error produce una cadena de errore entonces tenemos un contador maximo 
             * de 30 errores propagados al llegar al limite el parser se terminara*/

            String a = Dibujo.Form1.nombrearchivo;
            String b = "Lexico";
            String c = args.Token.Location.LineNr.ToString();
            String d = args.Token.Location.ColumnNr.ToString();
            String e = args.Token.ToString();

            Dibujo.Symbol m = new Dibujo.Symbol(a, b, c, d, e);
            Dibujo.Mi_Clase.error.AddLast(m);

            Dibujo.Mi_Clase.contador++;
            if (Dibujo.Mi_Clase.contador > 30)
            {
                args.Continue = false;
            }
            else
                args.Continue = true;

        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {

            //todo: Report message to UI?
            /*utilizamos el metodo de insewtar observar q la recuperacion es factible si el analizado
             espera un token especifico no token vectorial donde el analizador tiene o puede recibir varios token como una produccion que tiene diferenytes
             * dervaciones 
             esto mejor si no lo mensionas*/

            int c = args.UnexpectedToken.Location.LineNr;
            int d = args.UnexpectedToken.Location.ColumnNr;
            
            String a = Dibujo.Form1.nombrearchivo;
            String b = "Sintactico";
            String c1 = c.ToString();
            String d1 = d.ToString();
            String e = "la palabra incorrecta es .." + args.UnexpectedToken.ToString();



            Dibujo.Mi_Clase.contador++;
            if (Dibujo.Mi_Clase.contador > 30)
            {
                args.Continue = ContinueMode.Stop;
            }
            else
            {     //obtenemos el token siguiente si lo debujan se podra obsevar mejor

                String u = args.UnexpectedToken.ToString();//token faltante
                String df=args.ExpectedTokens.ToString();

                String[] g = df.Split(' ');//poque aveces cuasndo en lña gramatica se esperan varios token el df los devulve todos 
                //no solo el inmediato token que viene entonces solo se separael primero de la cadena si lo debujan entenderan mejor

                String tokensiguiente_nombre;
                tokensiguiente_nombre = g[0];
                


                int idtoken = 0;
                //total q contiene el enum de terminales o palabras resevadas 
                for (int i = 0; i < 27; i++)
                { //esto nos sirve pero si falta alguan palabra completa no para signos individuales
                    String z = Enum.GetName(typeof(SymbolConstants), i);
                    String t = z.Replace("SYMBOL_", "");
                    //lo convertimos a minusculas
                    t = t.ToLower();
                    if (t == tokensiguiente_nombre)
                    {
                        idtoken = i;
                        break;
                    }

                }
                //si es cero quiere decir que no era ninguno de los anteriores
                if (idtoken == 0)
                {
                    for (int vv = 0; vv < 3; vv++)
                    {
                        String k = Dibujo.Mi_Clase.e2.ElementAt(vv);
                        if (k == tokensiguiente_nombre)
                        {
                            idtoken = Dibujo.Mi_Clase.e1.ElementAt(vv);
                            break;
                        }

                    }
                }
                


                //ingresamos a la lista de errores
                Dibujo.Symbol m = new Dibujo.Symbol(a, b, c1, d1, e + " se esperaba " + tokensiguiente_nombre);
                Dibujo.Mi_Clase.error.AddLast(m);

                //posicion cero esto es de la line y columna del texto supuestamente en el archvo original ya que no viene lo ponumos
                //por default en 0,0,0
                Location nuevalocation = new Location(0, 0, 0);

                //insertamos el next token
                args.NextToken = new TerminalToken(new SymbolTerminal(idtoken, tokensiguiente_nombre),"", nuevalocation);
                args.Continue = ContinueMode.Insert;

            }
        }
    }
}
