
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF        =  0, // (EOF)
        SYMBOL_ERROR      =  1, // (Error)
        SYMBOL_COMMENT    =  2, // Comment
        SYMBOL_NEWLINE    =  3, // NewLine
        SYMBOL_WHITESPACE =  4, // Whitespace
        SYMBOL_DIV        =  5, // '/'
        SYMBOL_AMP        =  6, // '&'
        SYMBOL_COLON      =  7, // ':'
        SYMBOL_SEMI       =  8, // ';'
        SYMBOL_AMARILLO   =  9, // amarillo
        SYMBOL_CIRCULO    = 10, // circulo
        SYMBOL_CUADRADO   = 11, // cuadrado
        SYMBOL_FIGURA     = 12, // Figura
        SYMBOL_GRANDE     = 13, // grande
        SYMBOL_LETRA      = 14, // letra
        SYMBOL_MEDIANO    = 15, // mediano
        SYMBOL_NUMERO     = 16, // numero
        SYMBOL_RECTANGULO = 17, // rectangulo
        SYMBOL_ROJO       = 18, // rojo
        SYMBOL_TILDE      = 19, // tilde
        SYMBOL_TITULO     = 20, // Titulo
        SYMBOL_VERDE      = 21, // verde
        SYMBOL_COLOR      = 22, // <color>
        SYMBOL_FIGURA2    = 23, // <figura>
        SYMBOL_INICIO     = 24, // <inicio>
        SYMBOL_PALABRA    = 25, // <palabra>
        SYMBOL_SIZE       = 26, // <size>
        SYMBOL_TIPO       = 27  // <tipo>
    };

    enum RuleConstants : int
    {
        RULE_INICIO                                 =  0, // <inicio> ::= <figura>
        RULE_INICIO2                                =  1, // <inicio> ::= <inicio> <figura>
        RULE_FIGURA_FIGURA_COLON_COLON_AMP_SEMI     =  2, // <figura> ::= Figura ':' ':' <tipo> '&' <color> ';'
        RULE_FIGURA_TITULO_COLON_COLON_AMP_AMP_SEMI =  3, // <figura> ::= Titulo ':' ':' <palabra> '&' <color> '&' <size> ';'
        RULE_PALABRA_LETRA                          =  4, // <palabra> ::= letra
        RULE_PALABRA_NUMERO                         =  5, // <palabra> ::= numero
        RULE_PALABRA_TILDE                          =  6, // <palabra> ::= tilde
        RULE_PALABRA_LETRA2                         =  7, // <palabra> ::= <palabra> letra
        RULE_PALABRA_NUMERO2                        =  8, // <palabra> ::= <palabra> numero
        RULE_PALABRA_TILDE2                         =  9, // <palabra> ::= <palabra> tilde
        RULE_COLOR_ROJO                             = 10, // <color> ::= rojo
        RULE_COLOR_AMARILLO                         = 11, // <color> ::= amarillo
        RULE_COLOR_VERDE                            = 12, // <color> ::= verde
        RULE_TIPO_CUADRADO                          = 13, // <tipo> ::= cuadrado
        RULE_TIPO_RECTANGULO                        = 14, // <tipo> ::= rectangulo
        RULE_TIPO_CIRCULO                           = 15, // <tipo> ::= circulo
        RULE_SIZE_GRANDE                            = 16, // <size> ::= grande
        RULE_SIZE_MEDIANO                           = 17  // <size> ::= mediano
    };

    public class MyParser
    {
        private LALRParser parser;

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
                args.Continue = false;
                //todo: Report message to UI?
            }
        }

        private Object CreateObject(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMENT :
                //Comment
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NEWLINE :
                //NewLine
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_AMP :
                //'&'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SEMI :
                //';'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_AMARILLO :
                //amarillo
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CIRCULO :
                //circulo
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CUADRADO :
                //cuadrado
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FIGURA :
                //Figura
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GRANDE :
                //grande
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LETRA :
                //letra
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MEDIANO :
                //mediano
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NUMERO :
                //numero
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RECTANGULO :
                //rectangulo
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ROJO :
                //rojo
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TILDE :
                //tilde
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TITULO :
                //Titulo
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VERDE :
                //verde
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLOR :
                //<color>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FIGURA2 :
                //<figura>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INICIO :
                //<inicio>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PALABRA :
                //<palabra>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_SIZE :
                //<size>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIPO :
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
            {
                args.Continue = false;
                //todo: Report message to UI?
            }
        }

        public static Object CreateObject(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_INICIO :
                //<inicio> ::= <figura>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_INICIO2 :
                //<inicio> ::= <inicio> <figura>
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FIGURA_FIGURA_COLON_COLON_AMP_SEMI :
                //<figura> ::= Figura ':' ':' <tipo> '&' <color> ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_FIGURA_TITULO_COLON_COLON_AMP_AMP_SEMI :
                //<figura> ::= Titulo ':' ':' <palabra> '&' <color> '&' <size> ';'
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PALABRA_LETRA :
                //<palabra> ::= letra
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PALABRA_NUMERO :
                //<palabra> ::= numero
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PALABRA_TILDE :
                //<palabra> ::= tilde
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PALABRA_LETRA2 :
                //<palabra> ::= <palabra> letra
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PALABRA_NUMERO2 :
                //<palabra> ::= <palabra> numero
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_PALABRA_TILDE2 :
                //<palabra> ::= <palabra> tilde
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_COLOR_ROJO :
                //<color> ::= rojo
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_COLOR_AMARILLO :
                //<color> ::= amarillo
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_COLOR_VERDE :
                //<color> ::= verde
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_TIPO_CUADRADO :
                //<tipo> ::= cuadrado
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_TIPO_RECTANGULO :
                //<tipo> ::= rectangulo
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_TIPO_CIRCULO :
                //<tipo> ::= circulo
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SIZE_GRANDE :
                //<size> ::= grande
                //todo: Create a new object using the stored user objects.
                return null;

                case (int)RuleConstants.RULE_SIZE_MEDIANO :
                //<size> ::= mediano
                //todo: Create a new object using the stored user objects.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void AcceptEvent(LALRParser parser, AcceptEventArgs args)
        {
            //todo: Use your fully reduced args.Token.UserObject
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+"'";
            //todo: Report message to UI?
        }


    }
}
