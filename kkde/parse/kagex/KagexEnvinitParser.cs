// created by jay 0.7 (c) 1998 Axel.Schreiner@informatik.uni-osnabrueck.de

#line 2 "KagexEnvinitParser.jay"

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace kkde.parse.kagex
{
	//KAGEX envinit.tjsの構文解析クラス
	public class KagexEnvinitParser : kkde.parse.IParser
	{
		//パーサーで使用する変数
		private int yacc_verbose_flag = 1;
		KagexCompletionUnit m_cu;
		KagexEnvinitLexer m_lexer;
		
		#region プロパティ
		/// <summary>
		/// 解析結果
		/// </summary>
		public CompletionUnit CompletionUnit
		{
			get { return m_cu; }
		}
		#endregion
		
		//コンストラクタ
		public KagexEnvinitParser(string filePath, KagexEnvinitLexer lexer)
		{
			m_lexer = lexer;
			m_cu = new KagexCompletionUnit(filePath);
		}
		
		//パース実行
		public void Parse()
		{
			if (m_lexer == null)
			{
				return;
			}
			this.yyparse(m_lexer);
		}
#line default

  /** error output stream.
      It should be changeable.
    */
  public System.IO.TextWriter ErrorOutput = System.Console.Out;

  /** simplified error message.
      @see <a href="#yyerror(java.lang.String, java.lang.String[])">yyerror</a>
    */
  public void yyerror (string message) {
    yyerror(message, null);
  }

  /** (syntax) error message.
      Can be overwritten to control message format.
      @param message text to be displayed.
      @param expected vector of acceptable tokens, if available.
    */
  public void yyerror (string message, string[] expected) {
    if ((yacc_verbose_flag > 0) && (expected != null) && (expected.Length  > 0)) {
      ErrorOutput.Write (message+", expecting");
      for (int n = 0; n < expected.Length; ++ n)
        ErrorOutput.Write (" "+expected[n]);
        ErrorOutput.WriteLine ();
    } else
      ErrorOutput.WriteLine (message);
  }

  /** debugging support, requires the package jay.yydebug.
      Set to null to suppress debugging messages.
    */
//t  internal yydebug.yyDebug debug;

  protected static  int yyFinal = 1;
//t // Put this array into a separate class so it is only initialized if debugging is actually used
//t // Use MarshalByRefObject to disable inlining
//t class YYRules : MarshalByRefObject {
//t  public static  string [] yyRule = {
//t    "$accept : program",
//t    "program : global_list",
//t    "$$1 :",
//t    "global_list : $$1 def_list",
//t    "def_list :",
//t    "def_list : def_list block_or_statement",
//t    "def_list : def_list error T_SEMICOLON",
//t    "block_or_statement : statement",
//t    "block_or_statement : block",
//t    "statement : T_SEMICOLON",
//t    "statement : expr T_SEMICOLON",
//t    "statement : if",
//t    "statement : if_else",
//t    "statement : while",
//t    "statement : do_while",
//t    "statement : for",
//t    "statement : T_BREAK T_SEMICOLON",
//t    "statement : T_CONTINUE T_SEMICOLON",
//t    "statement : T_DEBUGGER T_SEMICOLON",
//t    "statement : variable_def",
//t    "statement : func_def",
//t    "statement : property_def",
//t    "statement : class_def",
//t    "statement : return",
//t    "statement : switch",
//t    "statement : with",
//t    "statement : case",
//t    "statement : try",
//t    "statement : throw",
//t    "$$2 :",
//t    "block : T_LBRACE $$2 def_list T_RBRACE",
//t    "$$3 :",
//t    "$$4 :",
//t    "while : T_WHILE $$3 T_LPARENTHESIS expr T_RPARENTHESIS $$4 block_or_statement",
//t    "$$5 :",
//t    "$$6 :",
//t    "do_while : T_DO $$5 block_or_statement T_WHILE T_LPARENTHESIS expr T_RPARENTHESIS $$6 T_SEMICOLON",
//t    "$$7 :",
//t    "$$8 :",
//t    "if : T_IF T_LPARENTHESIS $$7 expr $$8 T_RPARENTHESIS block_or_statement",
//t    "$$9 :",
//t    "if_else : if T_ELSE $$9 block_or_statement",
//t    "for : T_FOR T_LPARENTHESIS for_first_clause T_SEMICOLON for_second_clause T_SEMICOLON for_third_clause T_RPARENTHESIS block_or_statement",
//t    "for_first_clause :",
//t    "$$10 :",
//t    "for_first_clause : $$10 variable_def_inner",
//t    "for_first_clause : expr",
//t    "for_second_clause :",
//t    "for_second_clause : expr",
//t    "for_third_clause :",
//t    "for_third_clause : expr",
//t    "variable_def : variable_def_inner T_SEMICOLON",
//t    "variable_def_inner : T_VAR variable_id_list",
//t    "variable_def_inner : T_CONST variable_id_list",
//t    "variable_id_list : variable_id",
//t    "variable_id_list : variable_id_list T_COMMA variable_id",
//t    "variable_id : T_SYMBOL",
//t    "variable_id : T_SYMBOL T_EQUAL expr_no_comma",
//t    "$$11 :",
//t    "func_def : T_FUNCTION T_SYMBOL $$11 func_decl_arg_opt block",
//t    "$$12 :",
//t    "func_expr_def : T_FUNCTION $$12 func_decl_arg_opt block",
//t    "func_decl_arg_opt :",
//t    "func_decl_arg_opt : T_LPARENTHESIS func_decl_arg_collapse T_RPARENTHESIS",
//t    "func_decl_arg_opt : T_LPARENTHESIS func_decl_arg_list T_RPARENTHESIS",
//t    "func_decl_arg_opt : T_LPARENTHESIS func_decl_arg_at_least_one T_COMMA func_decl_arg_collapse T_RPARENTHESIS",
//t    "func_decl_arg_list :",
//t    "func_decl_arg_list : func_decl_arg_at_least_one",
//t    "func_decl_arg_at_least_one : func_decl_arg",
//t    "func_decl_arg_at_least_one : func_decl_arg_at_least_one T_COMMA func_decl_arg",
//t    "func_decl_arg : T_SYMBOL",
//t    "func_decl_arg : T_SYMBOL T_EQUAL expr_no_comma",
//t    "func_decl_arg_collapse : T_ASTERISK",
//t    "func_decl_arg_collapse : T_SYMBOL T_ASTERISK",
//t    "$$13 :",
//t    "property_def : T_PROPERTY T_SYMBOL T_LBRACE $$13 property_handler_def_list T_RBRACE",
//t    "property_handler_def_list : property_handler_setter",
//t    "property_handler_def_list : property_handler_getter",
//t    "property_handler_def_list : property_handler_setter property_handler_getter",
//t    "property_handler_def_list : property_handler_getter property_handler_setter",
//t    "$$14 :",
//t    "property_handler_setter : T_SETTER T_LPARENTHESIS T_SYMBOL T_RPARENTHESIS $$14 block",
//t    "$$15 :",
//t    "property_handler_getter : property_getter_handler_head $$15 block",
//t    "property_getter_handler_head : T_GETTER T_LPARENTHESIS T_RPARENTHESIS",
//t    "property_getter_handler_head : T_GETTER",
//t    "$$16 :",
//t    "class_def : T_CLASS T_SYMBOL $$16 class_extender block",
//t    "class_extender :",
//t    "class_extender : T_EXTENDS expr_no_comma",
//t    "$$17 :",
//t    "class_extender : T_EXTENDS expr_no_comma T_COMMA $$17 extends_list",
//t    "extends_list : extends_name",
//t    "extends_list : extends_list T_COMMA extends_name",
//t    "extends_name : expr_no_comma",
//t    "return : T_RETURN T_SEMICOLON",
//t    "return : T_RETURN expr T_SEMICOLON",
//t    "$$18 :",
//t    "switch : T_SWITCH T_LPARENTHESIS expr T_RPARENTHESIS $$18 block",
//t    "$$19 :",
//t    "with : T_WITH T_LPARENTHESIS expr T_RPARENTHESIS $$19 block_or_statement",
//t    "case : T_CASE expr T_COLON",
//t    "case : T_DEFAULT T_COLON",
//t    "$$20 :",
//t    "try : T_TRY $$20 block_or_statement catch block_or_statement",
//t    "catch : T_CATCH",
//t    "catch : T_CATCH T_LPARENTHESIS T_RPARENTHESIS",
//t    "catch : T_CATCH T_LPARENTHESIS T_SYMBOL T_RPARENTHESIS",
//t    "throw : T_THROW expr T_SEMICOLON",
//t    "expr_no_comma : assign_expr",
//t    "expr : comma_expr",
//t    "expr : comma_expr T_IF expr",
//t    "comma_expr : assign_expr",
//t    "comma_expr : comma_expr T_COMMA assign_expr",
//t    "assign_expr : cond_expr",
//t    "assign_expr : cond_expr T_SWAP assign_expr",
//t    "assign_expr : cond_expr T_EQUAL assign_expr",
//t    "assign_expr : cond_expr T_AMPERSANDEQUAL assign_expr",
//t    "assign_expr : cond_expr T_VERTLINEEQUAL assign_expr",
//t    "assign_expr : cond_expr T_CHEVRONEQUAL assign_expr",
//t    "assign_expr : cond_expr T_MINUSEQUAL assign_expr",
//t    "assign_expr : cond_expr T_PLUSEQUAL assign_expr",
//t    "assign_expr : cond_expr T_PERCENTEQUAL assign_expr",
//t    "assign_expr : cond_expr T_SLASHEQUAL assign_expr",
//t    "assign_expr : cond_expr T_BACKSLASHEQUAL assign_expr",
//t    "assign_expr : cond_expr T_ASTERISKEQUAL assign_expr",
//t    "assign_expr : cond_expr T_LOGICALOREQUAL assign_expr",
//t    "assign_expr : cond_expr T_LOGICALANDEQUAL assign_expr",
//t    "assign_expr : cond_expr T_RBITSHIFTEQUAL assign_expr",
//t    "assign_expr : cond_expr T_LARITHSHIFTEQUAL assign_expr",
//t    "assign_expr : cond_expr T_RARITHSHIFTEQUAL assign_expr",
//t    "cond_expr : logical_or_expr",
//t    "cond_expr : logical_or_expr T_QUESTION cond_expr T_COLON cond_expr",
//t    "logical_or_expr : logical_and_expr",
//t    "logical_or_expr : logical_or_expr T_LOGICALOR logical_and_expr",
//t    "logical_and_expr : inclusive_or_expr",
//t    "logical_and_expr : logical_and_expr T_LOGICALAND inclusive_or_expr",
//t    "inclusive_or_expr : exclusive_or_expr",
//t    "inclusive_or_expr : inclusive_or_expr T_VERTLINE exclusive_or_expr",
//t    "exclusive_or_expr : and_expr",
//t    "exclusive_or_expr : exclusive_or_expr T_CHEVRON and_expr",
//t    "and_expr : identical_expr",
//t    "and_expr : and_expr T_AMPERSAND identical_expr",
//t    "identical_expr : compare_expr",
//t    "identical_expr : identical_expr T_NOTEQUAL compare_expr",
//t    "identical_expr : identical_expr T_EQUALEQUAL compare_expr",
//t    "identical_expr : identical_expr T_DISCNOTEQUAL compare_expr",
//t    "identical_expr : identical_expr T_DISCEQUAL compare_expr",
//t    "compare_expr : shift_expr",
//t    "compare_expr : compare_expr T_LT shift_expr",
//t    "compare_expr : compare_expr T_GT shift_expr",
//t    "compare_expr : compare_expr T_LTOREQUAL shift_expr",
//t    "compare_expr : compare_expr T_GTOREQUAL shift_expr",
//t    "shift_expr : add_sub_expr",
//t    "shift_expr : shift_expr T_RARITHSHIFT add_sub_expr",
//t    "shift_expr : shift_expr T_LARITHSHIFT add_sub_expr",
//t    "shift_expr : shift_expr T_RBITSHIFT add_sub_expr",
//t    "add_sub_expr : mul_div_expr",
//t    "add_sub_expr : add_sub_expr T_PLUS mul_div_expr",
//t    "add_sub_expr : add_sub_expr T_MINUS mul_div_expr",
//t    "mul_div_expr : unary_expr",
//t    "mul_div_expr : mul_div_expr T_PERCENT unary_expr",
//t    "mul_div_expr : mul_div_expr T_SLASH unary_expr",
//t    "mul_div_expr : mul_div_expr T_BACKSLASH unary_expr",
//t    "mul_div_expr : mul_div_expr_and_asterisk unary_expr",
//t    "mul_div_expr_and_asterisk : mul_div_expr T_ASTERISK",
//t    "unary_expr : incontextof_expr",
//t    "unary_expr : T_EXCRAMATION unary_expr",
//t    "unary_expr : T_TILDE unary_expr",
//t    "unary_expr : T_DECREMENT unary_expr",
//t    "unary_expr : T_INCREMENT unary_expr",
//t    "unary_expr : T_NEW func_call_expr",
//t    "unary_expr : T_INVALIDATE unary_expr",
//t    "unary_expr : T_ISVALID unary_expr",
//t    "unary_expr : incontextof_expr T_ISVALID",
//t    "unary_expr : T_DELETE unary_expr",
//t    "unary_expr : T_TYPEOF unary_expr",
//t    "unary_expr : T_SHARP unary_expr",
//t    "unary_expr : T_DOLLAR unary_expr",
//t    "unary_expr : T_PLUS unary_expr",
//t    "unary_expr : T_MINUS unary_expr",
//t    "unary_expr : T_AMPERSAND unary_expr",
//t    "unary_expr : T_ASTERISK unary_expr",
//t    "unary_expr : incontextof_expr T_INSTANCEOF unary_expr",
//t    "unary_expr : T_LPARENTHESIS T_INT T_RPARENTHESIS unary_expr",
//t    "unary_expr : T_INT unary_expr",
//t    "unary_expr : T_LPARENTHESIS T_REAL T_RPARENTHESIS unary_expr",
//t    "unary_expr : T_REAL unary_expr",
//t    "unary_expr : T_LPARENTHESIS T_STRING T_RPARENTHESIS unary_expr",
//t    "unary_expr : T_STRING unary_expr",
//t    "incontextof_expr : priority_expr",
//t    "incontextof_expr : priority_expr T_INCONTEXTOF incontextof_expr",
//t    "priority_expr : factor_expr",
//t    "priority_expr : T_LPARENTHESIS expr T_RPARENTHESIS",
//t    "priority_expr : priority_expr T_LBRACKET expr T_RBRACKET",
//t    "priority_expr : func_call_expr",
//t    "$$21 :",
//t    "priority_expr : priority_expr T_DOT $$21 T_SYMBOL",
//t    "priority_expr : priority_expr T_INCREMENT",
//t    "priority_expr : priority_expr T_DECREMENT",
//t    "priority_expr : priority_expr T_EXCRAMATION",
//t    "$$22 :",
//t    "priority_expr : T_DOT $$22 T_SYMBOL",
//t    "factor_expr : T_CONSTVAL",
//t    "factor_expr : T_SYMBOL",
//t    "factor_expr : T_THIS",
//t    "factor_expr : T_SUPER",
//t    "factor_expr : func_expr_def",
//t    "factor_expr : T_GLOBAL",
//t    "factor_expr : T_VOID",
//t    "factor_expr : inline_array",
//t    "factor_expr : inline_dic",
//t    "factor_expr : const_inline_array",
//t    "factor_expr : const_inline_dic",
//t    "$$23 :",
//t    "factor_expr : T_SLASHEQUAL $$23 T_REGEXP",
//t    "$$24 :",
//t    "factor_expr : T_SLASH $$24 T_REGEXP",
//t    "func_call_expr : priority_expr T_LPARENTHESIS call_arg_list T_RPARENTHESIS",
//t    "call_arg_list : T_OMIT",
//t    "call_arg_list : call_arg",
//t    "call_arg_list : call_arg_list T_COMMA call_arg",
//t    "call_arg :",
//t    "call_arg : T_ASTERISK",
//t    "call_arg : mul_div_expr_and_asterisk",
//t    "call_arg : expr_no_comma",
//t    "$$25 :",
//t    "inline_array : T_LBRACKET $$25 array_elm_list T_RBRACKET",
//t    "array_elm_list : array_elm",
//t    "array_elm_list : array_elm_list T_COMMA array_elm",
//t    "array_elm :",
//t    "array_elm : expr_no_comma",
//t    "$$26 :",
//t    "inline_dic : T_PERCENT T_LBRACKET $$26 dic_elm_list dic_dummy_elm_opt T_RBRACKET",
//t    "dic_elm_list :",
//t    "dic_elm_list : dic_elm",
//t    "dic_elm_list : dic_elm_list T_COMMA dic_elm",
//t    "dic_elm : expr_no_comma T_COMMA expr_no_comma",
//t    "dic_elm : T_SYMBOL T_COLON expr_no_comma",
//t    "dic_dummy_elm_opt :",
//t    "dic_dummy_elm_opt : T_COMMA",
//t    "$$27 :",
//t    "const_inline_array : T_LPARENTHESIS T_CONST T_RPARENTHESIS T_LBRACKET $$27 const_array_elm_list_opt T_RBRACKET",
//t    "const_array_elm_list_opt :",
//t    "const_array_elm_list_opt : const_array_elm_list",
//t    "const_array_elm_list : const_array_elm",
//t    "const_array_elm_list : const_array_elm_list T_COMMA const_array_elm",
//t    "const_array_elm : T_MINUS T_CONSTVAL",
//t    "const_array_elm : T_PLUS T_CONSTVAL",
//t    "const_array_elm : T_CONSTVAL",
//t    "const_array_elm : T_VOID",
//t    "const_array_elm : const_inline_array",
//t    "const_array_elm : const_inline_dic",
//t    "$$28 :",
//t    "const_inline_dic : T_LPARENTHESIS T_CONST T_RPARENTHESIS T_PERCENT T_LBRACKET $$28 const_dic_elm_list T_RBRACKET",
//t    "const_dic_elm_list :",
//t    "const_dic_elm_list : const_dic_elm",
//t    "const_dic_elm_list : const_dic_elm_list T_COMMA const_dic_elm",
//t    "const_dic_elm : T_CONSTVAL T_COMMA T_MINUS T_CONSTVAL",
//t    "const_dic_elm : T_CONSTVAL T_COMMA T_PLUS T_CONSTVAL",
//t    "const_dic_elm : T_CONSTVAL T_COMMA T_CONSTVAL",
//t    "const_dic_elm : T_CONSTVAL T_COMMA T_VOID",
//t    "const_dic_elm : T_CONSTVAL T_COMMA const_inline_array",
//t    "const_dic_elm : T_CONSTVAL T_COMMA const_inline_dic",
//t  };
//t public static string getRule (int index) {
//t    return yyRule [index];
//t }
//t}
  protected static  string [] yyNames = {    
    "end-of-file",null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,"T_COMMA","T_EQUAL",
    "T_AMPERSANDEQUAL","T_VERTLINEEQUAL","T_CHEVRONEQUAL","T_MINUSEQUAL",
    "T_PLUSEQUAL","T_PERCENTEQUAL","T_SLASHEQUAL","T_BACKSLASHEQUAL",
    "T_ASTERISKEQUAL","T_LOGICALOREQUAL","T_LOGICALANDEQUAL",
    "T_RARITHSHIFTEQUAL","T_LARITHSHIFTEQUAL","T_RBITSHIFTEQUAL",
    "T_QUESTION","T_LOGICALOR","T_LOGICALAND","T_VERTLINE","T_CHEVRON",
    "T_AMPERSAND","T_NOTEQUAL","T_EQUALEQUAL","T_DISCNOTEQUAL",
    "T_DISCEQUAL","T_SWAP","T_LT","T_GT","T_LTOREQUAL","T_GTOREQUAL",
    "T_RARITHSHIFT","T_LARITHSHIFT","T_RBITSHIFT","T_PERCENT","T_SLASH",
    "T_BACKSLASH","T_ASTERISK","T_EXCRAMATION","T_TILDE","T_DECREMENT",
    "T_INCREMENT","T_NEW","T_DELETE","T_TYPEOF","T_PLUS","T_MINUS",
    "T_SHARP","T_DOLLAR","T_ISVALID","T_INVALIDATE","T_INSTANCEOF",
    "T_LPARENTHESIS","T_DOT","T_LBRACKET","T_THIS","T_SUPER","T_GLOBAL",
    "T_RBRACKET","T_CLASS","T_RPARENTHESIS","T_COLON","T_SEMICOLON",
    "T_LBRACE","T_RBRACE","T_CONTINUE","T_FUNCTION","T_DEBUGGER",
    "T_DEFAULT","T_CASE","T_EXTENDS","T_FINALLY","T_PROPERTY","T_PRIVATE",
    "T_PUBLIC","T_PROTECTED","T_STATIC","T_RETURN","T_BREAK","T_EXPORT",
    "T_IMPORT","T_SWITCH","T_IN","T_INCONTEXTOF","T_FOR","T_WHILE","T_DO",
    "T_IF","T_VAR","T_CONST","T_ENUM","T_GOTO","T_THROW","T_TRY",
    "T_SETTER","T_GETTER","T_ELSE","T_CATCH","T_OMIT","T_SYNCHRONIZED",
    "T_WITH","T_INT","T_REAL","T_STRING","T_OCTET","T_FALSE","T_NULL",
    "T_TRUE","T_VOID","T_NAN","T_INFINITY","T_UPLUS","T_UMINUS","T_EVAL",
    "T_POSTDECREMENT","T_POSTINCREMENT","T_IGNOREPROP","T_PROPACCESS",
    "T_ARG","T_EXPANDARG","T_INLINEARRAY","T_ARRAYARG","T_INLINEDIC",
    "T_DICELM","T_WITHDOT","T_THIS_PROXY","T_WITHDOT_PROXY","T_CONSTVAL",
    "T_SYMBOL","T_REGEXP",
  };

  /** index-checked interface to yyNames[].
      @param token single character or %token value.
      @return token name or [illegal] or [unknown].
    */
//t  public static string yyname (int token) {
//t    if ((token < 0) || (token > yyNames.Length)) return "[illegal]";
//t    string name;
//t    if ((name = yyNames[token]) != null) return name;
//t    return "[unknown]";
//t  }

  /** computes list of expected tokens on error by tracing the tables.
      @param state for which to compute the list.
      @return list of token names.
    */
  protected string[] yyExpecting (int state) {
    int token, n, len = 0;
    bool[] ok = new bool[yyNames.Length];

    if ((n = yySindex[state]) != 0)
      for (token = n < 0 ? -n : 0;
           (token < yyNames.Length) && (n+token < yyTable.Length); ++ token)
        if (yyCheck[n+token] == token && !ok[token] && yyNames[token] != null) {
          ++ len;
          ok[token] = true;
        }
    if ((n = yyRindex[state]) != 0)
      for (token = n < 0 ? -n : 0;
           (token < yyNames.Length) && (n+token < yyTable.Length); ++ token)
        if (yyCheck[n+token] == token && !ok[token] && yyNames[token] != null) {
          ++ len;
          ok[token] = true;
        }

    string [] result = new string[len];
    for (n = token = 0; n < len;  ++ token)
      if (ok[token]) result[n++] = yyNames[token];
    return result;
  }

  /** the generated parser, with debugging messages.
      Maintains a state and a value stack, currently with fixed maximum size.
      @param yyLex scanner.
      @param yydebug debug message writer implementing yyDebug, or null.
      @return result of the last reduction, if any.
      @throws yyException on irrecoverable parse error.
    */
  internal Object yyparse (yyParser.yyInput yyLex, Object yyd)
				 {
//t    this.debug = (yydebug.yyDebug)yyd;
    return yyparse(yyLex);
  }

  /** initial size and increment of the state/value stack [default 256].
      This is not final so that it can be overwritten outside of invocations
      of yyparse().
    */
  protected int yyMax;

  /** executed at the beginning of a reduce action.
      Used as $$ = yyDefault($1), prior to the user-specified action, if any.
      Can be overwritten to provide deep copy, etc.
      @param first value for $1, or null.
      @return first.
    */
  protected Object yyDefault (Object first) {
    return first;
  }

  /** the generated parser.
      Maintains a state and a value stack, currently with fixed maximum size.
      @param yyLex scanner.
      @return result of the last reduction, if any.
      @throws yyException on irrecoverable parse error.
    */
  internal Object yyparse (yyParser.yyInput yyLex)
  {
    if (yyMax <= 0) yyMax = 256;			// initial size
    int yyState = 0;                                   // state stack ptr
    int [] yyStates = new int[yyMax];	                // state stack 
    Object yyVal = null;                               // value stack ptr
    Object [] yyVals = new Object[yyMax];	        // value stack
    int yyToken = -1;					// current input
    int yyErrorFlag = 0;				// #tks to shift

    /*yyLoop:*/ for (int yyTop = 0;; ++ yyTop) {
      if (yyTop >= yyStates.Length) {			// dynamically increase
        int[] i = new int[yyStates.Length+yyMax];
        yyStates.CopyTo (i, 0);
        yyStates = i;
        Object[] o = new Object[yyVals.Length+yyMax];
        yyVals.CopyTo (o, 0);
        yyVals = o;
      }
      yyStates[yyTop] = yyState;
      yyVals[yyTop] = yyVal;
//t      if (debug != null) debug.push(yyState, yyVal);

      /*yyDiscarded:*/ for (;;) {	// discarding a token does not change stack
        int yyN;
        if ((yyN = yyDefRed[yyState]) == 0) {	// else [default] reduce (yyN)
          if (yyToken < 0) {
            yyToken = yyLex.advance() ? yyLex.token() : 0;
//t            if (debug != null)
//t              debug.lex(yyState, yyToken, yyname(yyToken), yyLex.value());
          }
          if ((yyN = yySindex[yyState]) != 0 && ((yyN += yyToken) >= 0)
              && (yyN < yyTable.Length) && (yyCheck[yyN] == yyToken)) {
//t            if (debug != null)
//t              debug.shift(yyState, yyTable[yyN], yyErrorFlag-1);
            yyState = yyTable[yyN];		// shift to yyN
            yyVal = yyLex.value();
            yyToken = -1;
            if (yyErrorFlag > 0) -- yyErrorFlag;
            goto continue_yyLoop;
          }
          if ((yyN = yyRindex[yyState]) != 0 && (yyN += yyToken) >= 0
              && yyN < yyTable.Length && yyCheck[yyN] == yyToken)
            yyN = yyTable[yyN];			// reduce (yyN)
          else
            switch (yyErrorFlag) {
  
            case 0:
              // yyerror(String.Format ("syntax error, got token `{0}'", yyname (yyToken)), yyExpecting(yyState));
//t              if (debug != null) debug.error("syntax error");
              goto case 1;
            case 1: case 2:
              yyErrorFlag = 3;
              do {
                if ((yyN = yySindex[yyStates[yyTop]]) != 0
                    && (yyN += Token.yyErrorCode) >= 0 && yyN < yyTable.Length
                    && yyCheck[yyN] == Token.yyErrorCode) {
//t                  if (debug != null)
//t                    debug.shift(yyStates[yyTop], yyTable[yyN], 3);
                  yyState = yyTable[yyN];
                  yyVal = yyLex.value();
                  goto continue_yyLoop;
                }
//t                if (debug != null) debug.pop(yyStates[yyTop]);
              } while (-- yyTop >= 0);
//t              if (debug != null) debug.reject();
              throw new yyParser.yyException("irrecoverable syntax error");
  
            case 3:
              if (yyToken == 0) {
//t                if (debug != null) debug.reject();
                throw new yyParser.yyException("irrecoverable syntax error at end-of-file");
              }
//t              if (debug != null)
//t                debug.discard(yyState, yyToken, yyname(yyToken),
//t  							yyLex.value());
              yyToken = -1;
              goto continue_yyDiscarded;		// leave stack alone
            }
        }
        int yyV = yyTop + 1-yyLen[yyN];
//t        if (debug != null)
//t          debug.reduce(yyState, yyStates[yyV-1], yyN, YYRules.getRule (yyN), yyLen[yyN]);
        yyVal = yyDefault(yyV > yyTop ? null : yyVals[yyV]);
        switch (yyN) {
case 2:
#line 201 "KagexEnvinitParser.jay"
  {}
  break;
case 3:
#line 202 "KagexEnvinitParser.jay"
  {}
  break;
case 6:
#line 209 "KagexEnvinitParser.jay"
  {}
  break;
case 10:
#line 221 "KagexEnvinitParser.jay"
  {}
  break;
case 16:
#line 227 "KagexEnvinitParser.jay"
  {}
  break;
case 17:
#line 228 "KagexEnvinitParser.jay"
  {}
  break;
case 18:
#line 229 "KagexEnvinitParser.jay"
  {}
  break;
case 29:
#line 244 "KagexEnvinitParser.jay"
  {}
  break;
case 30:
#line 246 "KagexEnvinitParser.jay"
  {}
  break;
case 31:
#line 251 "KagexEnvinitParser.jay"
  {}
  break;
case 32:
#line 252 "KagexEnvinitParser.jay"
  {}
  break;
case 33:
#line 253 "KagexEnvinitParser.jay"
  {}
  break;
case 34:
#line 258 "KagexEnvinitParser.jay"
  {}
  break;
case 35:
#line 261 "KagexEnvinitParser.jay"
  {}
  break;
case 36:
#line 262 "KagexEnvinitParser.jay"
  {}
  break;
case 37:
#line 267 "KagexEnvinitParser.jay"
  {}
  break;
case 38:
#line 268 "KagexEnvinitParser.jay"
  {}
  break;
case 39:
#line 269 "KagexEnvinitParser.jay"
  {}
  break;
case 40:
#line 274 "KagexEnvinitParser.jay"
  {}
  break;
case 41:
#line 275 "KagexEnvinitParser.jay"
  {}
  break;
case 42:
#line 284 "KagexEnvinitParser.jay"
  {}
  break;
case 43:
#line 290 "KagexEnvinitParser.jay"
  {}
  break;
case 44:
#line 291 "KagexEnvinitParser.jay"
  {}
  break;
case 46:
#line 293 "KagexEnvinitParser.jay"
  {}
  break;
case 47:
#line 298 "KagexEnvinitParser.jay"
  {}
  break;
case 48:
#line 299 "KagexEnvinitParser.jay"
  {}
  break;
case 49:
#line 304 "KagexEnvinitParser.jay"
  {}
  break;
case 50:
#line 305 "KagexEnvinitParser.jay"
  {}
  break;
case 56:
#line 328 "KagexEnvinitParser.jay"
  {}
  break;
case 57:
#line 329 "KagexEnvinitParser.jay"
  {}
  break;
case 58:
#line 334 "KagexEnvinitParser.jay"
  {}
  break;
case 59:
#line 336 "KagexEnvinitParser.jay"
  {}
  break;
case 60:
#line 341 "KagexEnvinitParser.jay"
  {}
  break;
case 61:
#line 343 "KagexEnvinitParser.jay"
  {}
  break;
case 70:
#line 366 "KagexEnvinitParser.jay"
  {}
  break;
case 71:
#line 367 "KagexEnvinitParser.jay"
  {}
  break;
case 72:
#line 371 "KagexEnvinitParser.jay"
  {}
  break;
case 73:
#line 372 "KagexEnvinitParser.jay"
  {}
  break;
case 74:
#line 383 "KagexEnvinitParser.jay"
  {}
  break;
case 75:
#line 385 "KagexEnvinitParser.jay"
  {}
  break;
case 80:
#line 396 "KagexEnvinitParser.jay"
  {}
  break;
case 81:
#line 397 "KagexEnvinitParser.jay"
  {}
  break;
case 82:
#line 401 "KagexEnvinitParser.jay"
  {}
  break;
case 83:
#line 402 "KagexEnvinitParser.jay"
  {}
  break;
case 86:
#line 413 "KagexEnvinitParser.jay"
  {}
  break;
case 87:
#line 415 "KagexEnvinitParser.jay"
  {}
  break;
case 89:
#line 420 "KagexEnvinitParser.jay"
  {}
  break;
case 90:
#line 421 "KagexEnvinitParser.jay"
  {}
  break;
case 94:
#line 431 "KagexEnvinitParser.jay"
  {}
  break;
case 95:
#line 436 "KagexEnvinitParser.jay"
  {}
  break;
case 96:
#line 437 "KagexEnvinitParser.jay"
  {}
  break;
case 97:
#line 444 "KagexEnvinitParser.jay"
  {}
  break;
case 98:
#line 445 "KagexEnvinitParser.jay"
  {}
  break;
case 99:
#line 451 "KagexEnvinitParser.jay"
  {}
  break;
case 100:
#line 452 "KagexEnvinitParser.jay"
  {}
  break;
case 101:
#line 457 "KagexEnvinitParser.jay"
  {}
  break;
case 102:
#line 458 "KagexEnvinitParser.jay"
  {}
  break;
case 103:
#line 463 "KagexEnvinitParser.jay"
  {}
  break;
case 104:
#line 466 "KagexEnvinitParser.jay"
  {}
  break;
case 105:
#line 470 "KagexEnvinitParser.jay"
  {}
  break;
case 106:
#line 471 "KagexEnvinitParser.jay"
  {}
  break;
case 107:
#line 472 "KagexEnvinitParser.jay"
  {}
  break;
case 108:
#line 477 "KagexEnvinitParser.jay"
  {}
  break;
case 109:
#line 482 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 110:
#line 486 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 111:
#line 487 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[-2+yyTop]);}
  break;
case 112:
#line 492 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 113:
#line 493 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 114:
#line 498 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 115:
#line 499 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[-2+yyTop]);}
  break;
case 116:
#line 500 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[-2+yyTop]);}
  break;
case 117:
#line 501 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[-2+yyTop]);}
  break;
case 118:
#line 502 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[-2+yyTop]);}
  break;
case 119:
#line 503 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[-2+yyTop]);}
  break;
case 120:
#line 504 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[-2+yyTop]);}
  break;
case 121:
#line 505 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[-2+yyTop]);}
  break;
case 122:
#line 506 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[-2+yyTop]);}
  break;
case 123:
#line 507 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[-2+yyTop]);}
  break;
case 124:
#line 508 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[-2+yyTop]);}
  break;
case 125:
#line 509 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[-2+yyTop]);}
  break;
case 126:
#line 510 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[-2+yyTop]);}
  break;
case 127:
#line 511 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[-2+yyTop]);}
  break;
case 128:
#line 512 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[-2+yyTop]);}
  break;
case 129:
#line 513 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[-2+yyTop]);}
  break;
case 130:
#line 514 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[-2+yyTop]);}
  break;
case 131:
#line 519 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 132:
#line 522 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[-4+yyTop]);}
  break;
case 133:
#line 528 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 134:
#line 529 "KagexEnvinitParser.jay"
  {}
  break;
case 135:
#line 533 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 136:
#line 535 "KagexEnvinitParser.jay"
  {}
  break;
case 137:
#line 539 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 138:
#line 540 "KagexEnvinitParser.jay"
  {}
  break;
case 139:
#line 544 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 140:
#line 545 "KagexEnvinitParser.jay"
  {}
  break;
case 141:
#line 549 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 142:
#line 550 "KagexEnvinitParser.jay"
  {}
  break;
case 143:
#line 554 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 144:
#line 555 "KagexEnvinitParser.jay"
  {}
  break;
case 145:
#line 556 "KagexEnvinitParser.jay"
  {}
  break;
case 146:
#line 557 "KagexEnvinitParser.jay"
  {}
  break;
case 147:
#line 558 "KagexEnvinitParser.jay"
  {}
  break;
case 148:
#line 562 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 149:
#line 563 "KagexEnvinitParser.jay"
  {}
  break;
case 150:
#line 564 "KagexEnvinitParser.jay"
  {}
  break;
case 151:
#line 565 "KagexEnvinitParser.jay"
  {}
  break;
case 152:
#line 566 "KagexEnvinitParser.jay"
  {}
  break;
case 153:
#line 570 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 154:
#line 571 "KagexEnvinitParser.jay"
  {}
  break;
case 155:
#line 572 "KagexEnvinitParser.jay"
  {}
  break;
case 156:
#line 573 "KagexEnvinitParser.jay"
  {}
  break;
case 157:
#line 578 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 158:
#line 579 "KagexEnvinitParser.jay"
  {}
  break;
case 159:
#line 580 "KagexEnvinitParser.jay"
  {}
  break;
case 160:
#line 584 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 161:
#line 585 "KagexEnvinitParser.jay"
  {}
  break;
case 162:
#line 586 "KagexEnvinitParser.jay"
  {}
  break;
case 163:
#line 587 "KagexEnvinitParser.jay"
  {}
  break;
case 164:
#line 588 "KagexEnvinitParser.jay"
  {}
  break;
case 165:
#line 592 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[-1+yyTop]);}
  break;
case 166:
#line 596 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 167:
#line 597 "KagexEnvinitParser.jay"
  {}
  break;
case 168:
#line 598 "KagexEnvinitParser.jay"
  {}
  break;
case 169:
#line 599 "KagexEnvinitParser.jay"
  {}
  break;
case 170:
#line 600 "KagexEnvinitParser.jay"
  {}
  break;
case 171:
#line 601 "KagexEnvinitParser.jay"
  {}
  break;
case 172:
#line 602 "KagexEnvinitParser.jay"
  {}
  break;
case 173:
#line 603 "KagexEnvinitParser.jay"
  {}
  break;
case 174:
#line 604 "KagexEnvinitParser.jay"
  {}
  break;
case 175:
#line 605 "KagexEnvinitParser.jay"
  {}
  break;
case 176:
#line 606 "KagexEnvinitParser.jay"
  {}
  break;
case 177:
#line 607 "KagexEnvinitParser.jay"
  {}
  break;
case 178:
#line 608 "KagexEnvinitParser.jay"
  {}
  break;
case 179:
#line 609 "KagexEnvinitParser.jay"
  {}
  break;
case 180:
#line 610 "KagexEnvinitParser.jay"
  {}
  break;
case 181:
#line 611 "KagexEnvinitParser.jay"
  {}
  break;
case 182:
#line 612 "KagexEnvinitParser.jay"
  {}
  break;
case 183:
#line 613 "KagexEnvinitParser.jay"
  {}
  break;
case 184:
#line 614 "KagexEnvinitParser.jay"
  {}
  break;
case 185:
#line 615 "KagexEnvinitParser.jay"
  {}
  break;
case 186:
#line 616 "KagexEnvinitParser.jay"
  {}
  break;
case 187:
#line 617 "KagexEnvinitParser.jay"
  {}
  break;
case 188:
#line 618 "KagexEnvinitParser.jay"
  {}
  break;
case 189:
#line 619 "KagexEnvinitParser.jay"
  {}
  break;
case 190:
#line 623 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 191:
#line 625 "KagexEnvinitParser.jay"
  {}
  break;
case 192:
#line 629 "KagexEnvinitParser.jay"
  {}
  break;
case 193:
#line 630 "KagexEnvinitParser.jay"
  {}
  break;
case 194:
#line 631 "KagexEnvinitParser.jay"
  {}
  break;
case 195:
#line 632 "KagexEnvinitParser.jay"
  {}
  break;
case 196:
#line 633 "KagexEnvinitParser.jay"
  {}
  break;
case 197:
#line 634 "KagexEnvinitParser.jay"
  {}
  break;
case 198:
#line 635 "KagexEnvinitParser.jay"
  {}
  break;
case 199:
#line 636 "KagexEnvinitParser.jay"
  {}
  break;
case 200:
#line 637 "KagexEnvinitParser.jay"
  {}
  break;
case 201:
#line 638 "KagexEnvinitParser.jay"
  {}
  break;
case 202:
#line 639 "KagexEnvinitParser.jay"
  {}
  break;
case 203:
#line 644 "KagexEnvinitParser.jay"
  {yyVal = ((string)yyVals[0+yyTop]);}
  break;
case 204:
#line 645 "KagexEnvinitParser.jay"
  {yyVal = ((string)yyVals[0+yyTop]);}
  break;
case 205:
#line 646 "KagexEnvinitParser.jay"
  {yyVal = ((int)yyVals[0+yyTop]);}
  break;
case 206:
#line 647 "KagexEnvinitParser.jay"
  {yyVal = ((int)yyVals[0+yyTop]);}
  break;
case 207:
#line 648 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 208:
#line 649 "KagexEnvinitParser.jay"
  {yyVal = ((int)yyVals[0+yyTop]);}
  break;
case 209:
#line 650 "KagexEnvinitParser.jay"
  {yyVal = ((int)yyVals[0+yyTop]);}
  break;
case 210:
#line 651 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 211:
#line 652 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 212:
#line 653 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 213:
#line 654 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 214:
#line 655 "KagexEnvinitParser.jay"
  {/*m_lexer.SetOnRegexFlag();*/}
  break;
case 215:
#line 656 "KagexEnvinitParser.jay"
  {yyVal = ((int)yyVals[-2+yyTop]);}
  break;
case 216:
#line 657 "KagexEnvinitParser.jay"
  {/*m_lexer.SetOnRegexFlag();*/}
  break;
case 217:
#line 658 "KagexEnvinitParser.jay"
  {yyVal = ((int)yyVals[-2+yyTop]);}
  break;
case 218:
#line 664 "KagexEnvinitParser.jay"
  {}
  break;
case 219:
#line 669 "KagexEnvinitParser.jay"
  {}
  break;
case 220:
#line 670 "KagexEnvinitParser.jay"
  {}
  break;
case 221:
#line 671 "KagexEnvinitParser.jay"
  {}
  break;
case 222:
#line 675 "KagexEnvinitParser.jay"
  {}
  break;
case 223:
#line 676 "KagexEnvinitParser.jay"
  {yyVal = ((int)yyVals[0+yyTop]);}
  break;
case 224:
#line 677 "KagexEnvinitParser.jay"
  {}
  break;
case 225:
#line 678 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 226:
#line 684 "KagexEnvinitParser.jay"
  {m_cu.PushArray();}
  break;
case 227:
#line 686 "KagexEnvinitParser.jay"
  {yyVal = m_cu.PeekArray(); m_cu.PopArray();}
  break;
case 228:
#line 691 "KagexEnvinitParser.jay"
  {m_cu.AddArray(((object)yyVals[0+yyTop]));}
  break;
case 229:
#line 692 "KagexEnvinitParser.jay"
  {m_cu.AddArray(((object)yyVals[0+yyTop]));}
  break;
case 230:
#line 697 "KagexEnvinitParser.jay"
  {}
  break;
case 231:
#line 698 "KagexEnvinitParser.jay"
  {yyVal = ((object)yyVals[0+yyTop]);}
  break;
case 232:
#line 703 "KagexEnvinitParser.jay"
  {m_cu.PushDic();}
  break;
case 233:
#line 706 "KagexEnvinitParser.jay"
  {yyVal = m_cu.PeekDic(); m_cu.PopDic();}
  break;
case 235:
#line 713 "KagexEnvinitParser.jay"
  {m_cu.AddDicItem((TableItem)((object)yyVals[0+yyTop]));}
  break;
case 236:
#line 714 "KagexEnvinitParser.jay"
  {m_cu.AddDicItem((TableItem)((object)yyVals[0+yyTop]));}
  break;
case 237:
#line 719 "KagexEnvinitParser.jay"
  {yyVal = m_cu.CreateDicItem(((object)yyVals[-2+yyTop]), ((object)yyVals[0+yyTop]));}
  break;
case 238:
#line 720 "KagexEnvinitParser.jay"
  {yyVal = m_cu.CreateDicItem(((string)yyVals[-2+yyTop]), ((object)yyVals[0+yyTop]));}
  break;
case 241:
#line 733 "KagexEnvinitParser.jay"
  {}
  break;
case 242:
#line 735 "KagexEnvinitParser.jay"
  {}
  break;
case 247:
#line 753 "KagexEnvinitParser.jay"
  {}
  break;
case 248:
#line 754 "KagexEnvinitParser.jay"
  {}
  break;
case 249:
#line 755 "KagexEnvinitParser.jay"
  {}
  break;
case 250:
#line 756 "KagexEnvinitParser.jay"
  {}
  break;
case 251:
#line 757 "KagexEnvinitParser.jay"
  {}
  break;
case 252:
#line 758 "KagexEnvinitParser.jay"
  {}
  break;
case 253:
#line 763 "KagexEnvinitParser.jay"
  {}
  break;
case 254:
#line 765 "KagexEnvinitParser.jay"
  {}
  break;
case 258:
#line 778 "KagexEnvinitParser.jay"
  {}
  break;
case 259:
#line 779 "KagexEnvinitParser.jay"
  {}
  break;
case 260:
#line 780 "KagexEnvinitParser.jay"
  {}
  break;
case 261:
#line 781 "KagexEnvinitParser.jay"
  {}
  break;
case 262:
#line 782 "KagexEnvinitParser.jay"
  {}
  break;
case 263:
#line 783 "KagexEnvinitParser.jay"
  {}
  break;
#line default
        }
        yyTop -= yyLen[yyN];
        yyState = yyStates[yyTop];
        int yyM = yyLhs[yyN];
        if (yyState == 0 && yyM == 0) {
//t          if (debug != null) debug.shift(0, yyFinal);
          yyState = yyFinal;
          if (yyToken < 0) {
            yyToken = yyLex.advance() ? yyLex.token() : 0;
//t            if (debug != null)
//t               debug.lex(yyState, yyToken,yyname(yyToken), yyLex.value());
          }
          if (yyToken == 0) {
//t            if (debug != null) debug.accept(yyVal);
            return yyVal;
          }
          goto continue_yyLoop;
        }
        if (((yyN = yyGindex[yyM]) != 0) && ((yyN += yyState) >= 0)
            && (yyN < yyTable.Length) && (yyCheck[yyN] == yyState))
          yyState = yyTable[yyN];
        else
          yyState = yyDgoto[yyM];
//t        if (debug != null) debug.shift(yyStates[yyTop], yyState);
	 goto continue_yyLoop;
      continue_yyDiscarded: continue;	// implements the named-loop continue: 'continue yyDiscarded'
      }
    continue_yyLoop: continue;		// implements the named-loop continue: 'continue yyLoop'
    }
  }

   static  short [] yyLhs  = {              -1,
    0,   33,   31,   32,   32,   32,   34,   34,   35,   35,
   35,   35,   35,   35,   35,   35,   35,   35,   35,   35,
   35,   35,   35,   35,   35,   35,   35,   35,   52,   36,
   53,   54,   39,   55,   56,   40,   57,   58,   37,   59,
   38,   41,   60,   64,   60,   60,   61,   61,   62,   62,
   42,   63,   63,   65,   65,   66,   66,   68,   43,   69,
   22,   67,   67,   67,   67,   71,   71,   72,   72,   73,
   73,   70,   70,   75,   44,   74,   74,   74,   74,   78,
   76,   80,   77,   79,   79,   82,   45,   81,   81,   84,
   81,   83,   83,   85,   46,   46,   86,   47,   87,   48,
   49,   49,   88,   50,   89,   89,   89,   51,   24,    1,
    1,    2,    2,    3,    3,    3,    3,    3,    3,    3,
    3,    3,    3,    3,    3,    3,    3,    3,    3,    3,
    4,    4,    5,    5,    6,    6,    7,    7,    8,    8,
    9,    9,   10,   10,   10,   10,   10,   11,   11,   11,
   11,   11,   12,   12,   12,   12,   13,   13,   13,   14,
   14,   14,   14,   14,   15,   16,   16,   16,   16,   16,
   16,   16,   16,   16,   16,   16,   16,   16,   16,   16,
   16,   16,   16,   16,   16,   16,   16,   16,   16,   17,
   17,   18,   18,   18,   18,   90,   18,   18,   18,   18,
   91,   18,   19,   19,   19,   19,   19,   19,   19,   19,
   19,   19,   19,   92,   19,   93,   19,   23,   21,   21,
   21,   20,   20,   20,   20,   95,   25,   94,   94,   26,
   26,   97,   27,   96,   96,   96,   28,   28,   98,   98,
  100,   29,   99,   99,  101,  101,  102,  102,  102,  102,
  102,  102,  104,   30,  103,  103,  103,  105,  105,  105,
  105,  105,  105,
  };
   static  short [] yyLen = {           2,
    1,    0,    2,    0,    2,    3,    1,    1,    1,    2,
    1,    1,    1,    1,    1,    2,    2,    2,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    0,    4,
    0,    0,    7,    0,    0,    9,    0,    0,    7,    0,
    4,    9,    0,    0,    2,    1,    0,    1,    0,    1,
    2,    2,    2,    1,    3,    1,    3,    0,    5,    0,
    4,    0,    3,    3,    5,    0,    1,    1,    3,    1,
    3,    1,    2,    0,    6,    1,    1,    2,    2,    0,
    6,    0,    3,    3,    1,    0,    5,    0,    2,    0,
    5,    1,    3,    1,    2,    3,    0,    6,    0,    6,
    3,    2,    0,    5,    1,    3,    4,    3,    1,    1,
    3,    1,    3,    1,    3,    3,    3,    3,    3,    3,
    3,    3,    3,    3,    3,    3,    3,    3,    3,    3,
    1,    5,    1,    3,    1,    3,    1,    3,    1,    3,
    1,    3,    1,    3,    3,    3,    3,    1,    3,    3,
    3,    3,    1,    3,    3,    3,    1,    3,    3,    1,
    3,    3,    3,    2,    2,    1,    2,    2,    2,    2,
    2,    2,    2,    2,    2,    2,    2,    2,    2,    2,
    2,    2,    3,    4,    2,    4,    2,    4,    2,    1,
    3,    1,    3,    4,    1,    0,    4,    2,    2,    2,
    0,    3,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    0,    3,    0,    3,    4,    1,    1,
    3,    0,    1,    1,    1,    0,    4,    1,    3,    0,
    1,    0,    6,    0,    1,    3,    3,    3,    0,    1,
    0,    7,    0,    1,    1,    3,    2,    2,    1,    1,
    1,    1,    0,    8,    0,    1,    3,    4,    4,    3,
    3,    3,    3,
  };
   static  short [] yyDefRed = {            2,
    0,    1,    4,    0,    0,  214,    0,    0,  216,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  201,  226,  205,  206,  208,    0,
    9,   29,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   31,   34,    0,    0,    0,    0,  103,    0,
    0,    0,    0,  209,  203,  204,    0,    0,  112,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  160,    0,    0,  192,  207,  195,  210,  211,  212,
  213,    5,    7,    8,    0,   12,   13,   14,   15,   19,
   20,   21,   22,   23,   24,   25,   26,   27,   28,    0,
    6,    0,   60,  181,  232,    0,  182,  167,  168,  169,
  170,    0,    0,    0,  175,  176,  179,  180,  177,  178,
  173,  172,    0,    0,    0,    0,    0,    0,    0,   86,
    4,   17,   58,    0,   18,  102,    0,    0,   95,    0,
   16,    0,    0,    0,    0,   37,    0,    0,   54,    0,
    0,    0,    0,  185,  187,  189,   10,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  165,  164,  174,
    0,  200,  199,  198,    0,  196,    0,    0,   40,   51,
  215,    0,  217,    0,    0,    0,    0,  193,  202,  109,
  231,  228,    0,    0,    0,    0,    0,    0,  101,   74,
   96,    0,   46,    0,    0,    0,    0,    0,    0,    0,
  108,    0,    0,  113,  111,  116,  117,  118,  119,  120,
  121,  122,  123,  124,  125,  126,  127,  130,  129,  128,
  115,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  161,  162,  163,  183,    0,  219,    0,  220,    0,  225,
    0,    0,  191,    0,    0,    0,  235,    0,    0,  241,
  184,  186,  188,    0,  227,    0,    0,   30,    0,   72,
    0,    0,    0,    0,   68,   61,    0,   97,    0,   45,
    0,    0,   38,   57,   55,    0,    0,   99,    0,    0,
  218,  197,  194,   41,    0,    0,    0,    0,  253,    0,
  229,    0,   87,   59,    0,   73,   63,   64,    0,    0,
    0,    0,    0,    0,   82,    0,   48,    0,   32,    0,
    0,    0,  104,    0,  132,  221,  238,  237,  236,  233,
    0,    0,    0,    0,  250,  249,  251,  252,    0,    0,
  245,   90,   71,    0,   69,    0,    0,   75,   78,   79,
    0,   98,    0,    0,    0,    0,  106,    0,  100,    0,
    0,  256,  248,  247,  242,    0,    0,   65,    0,   84,
   83,   50,    0,   33,   35,   39,  107,    0,    0,  254,
  246,   94,    0,   92,   80,    0,    0,    0,    0,  261,
  260,  262,  263,  257,    0,    0,   42,   36,  259,  258,
   93,   81,
  };
  protected static  short [] yyDgoto  = {             1,
   57,   58,   59,   60,   61,   62,   63,   64,   65,   66,
   67,   68,   69,   70,   71,   72,   73,   74,   75,  288,
  289,   76,   77,  221,   78,  222,   79,  297,   80,   81,
    2,    4,    3,   82,   83,   84,   85,   86,   87,   88,
   89,   90,   91,   92,   93,   94,   95,   96,   97,   98,
   99,  131,  144,  394,  145,  427,  238,  361,  294,  234,
  358,  413,  100,  235,  148,  149,  228,  226,  134,  312,
  313,  314,  315,  352,  317,  353,  354,  436,  355,  391,
  307,  224,  423,  407,  424,  356,  364,  152,  327,  291,
  128,  102,  106,  223,  129,  298,  212,  338,  379,  340,
  380,  381,  401,  371,  402,
  };
  protected static  short [] yySindex = {            0,
    0,    0,    0,  290, -305,    0, 1144, -293,    0, 1144,
 1144, 1144, 1144, 1144, -212, 1144, 1144, 1144, 1144, 1144,
 1144, 1144, 1144,  578,    0,    0,    0,    0,    0, -355,
    0,    0, -270, -326, -190, -198, 1144, -254,  674, -186,
 -173, -171,    0,    0, -164, -234, -234, 1144,    0, -147,
 1144, 1144, 1144,    0,    0,    0, -156, -251,    0,  134,
 -247, -105, -102, -106, -103, -121, -118,  -64, -260, -113,
 1144,    0, -291, -163,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, -148,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, -112,
    0, -175,    0,    0,    0, -174,    0,    0,    0,    0,
    0,  717,  -82,    0,    0,    0,    0,    0,    0,    0,
    0,    0, -100,  813,  909, 1005,  -99, -155, 1144,    0,
    0,    0,    0,  -78,    0,    0,  -84,  -80,    0,  -83,
    0, 1144, 1144,  -61,  386,    0,   -8,   -1,    0,   -1,
  -60,  386, 1144,    0,    0,    0,    0, 1144, 1144, 1144,
 1144, 1144, 1144, 1144, 1144, 1144, 1144, 1144, 1144, 1144,
 1144, 1144, 1144, 1144, 1144, 1144, 1144, 1144, 1144, 1144,
 1144, 1144, 1144, 1144, 1144, 1144, 1144, 1144, 1144, 1144,
 1144, 1144, 1144, 1144, 1144, 1144, 1144,    0,    0,    0,
 1144,    0,    0,    0, 1048,    0, 1144, -212,    0,    0,
    0, 1187,    0, -278, 1144, 1144, 1144,    0,    0,    0,
    0,    0, -203,  -69,  151,  -78, -289,  -59,    0,    0,
    0,  -57,    0,  -56, -294, 1144,  -77, 1144, 1144, -234,
    0,  -92,  -53,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  -51, -105, -102, -106, -103, -121, -118, -118, -118,
 -118,  -64,  -64,  -64,  -64, -260, -260, -260, -113, -113,
    0,    0,    0,    0, 1144,    0, 1144,    0, -233,    0,
 -117,  -46,    0,  386,  -47,   15,    0,   16,  -37,    0,
    0,    0,    0, 1144,    0, 1144,  -59,    0,  -59,    0,
 -250,  -42,  -41,   20,    0,    0, -197,    0, 1144,    0,
  -39,  -29,    0,    0,    0,  -27,  386,    0, 1144, 1283,
    0,    0,    0,    0, 1144, 1144, 1187,  -32,    0, -280,
    0,   27,    0,    0, 1144,    0,    0,    0, -289,  -24,
  -23,  -34,  -63,  -33,    0,  -59,    0,  -31,    0, 1144,
  -26, -307,    0,  386,    0,    0,    0,    0,    0,    0,
  -94,  -90,  -65,  -18,    0,    0,    0,    0,   14,   74,
    0,    0,    0,   17,    0,  -52,   21,    0,    0,    0,
  -59,    0, 1144,  386,   23,  386,    0,   24,    0,   75,
 -199,    0,    0,    0,    0, -280, 1144,    0,   31,    0,
    0,    0,   32,    0,    0,    0,    0, -262,  -94,    0,
    0,    0,   95,    0,    0,  386,   34,  -30,  -28,    0,
    0,    0,    0,    0, 1144,  -59,    0,    0,    0,    0,
    0,    0,
  };
  protected static  short [] yyRindex = {            0,
    0,    0,    0,  362,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, -300,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, -194,    0, -229,
 3289, 3223, 3157, 3091,  -74, 2971, 2675, 2292, 1940, 1676,
    0,    0, 1588, 1500,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    1,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0, 1412,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, -196,    0,
    0,    0,    0,   43,    0,    0,    0,    0,    0,    0,
    0,    0, -308,    0,    0,    0, -232,   46,    0,   53,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, -225,    0,    0,    0,    0,    0,
    0, -151,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   61,    0,   43,   65,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 3261, 3195, 3129, 3063, 2999, 2763, 2791, 2879,
 2907, 2380, 2468, 2556, 2644, 2028, 2116, 2204, 1764, 1852,
    0,    0,    0,    0, -222,    0, -209,    0,    0,    0,
    0,    0,    0,    0, 3377,    0,    0,   68,    0,    0,
    0,    0,    0, -196,    0,    0,    0,    0,    0,    0,
 -207,    0,    0,   67,    0,    0,    0,    0,   69,    0,
    0,    0,    0,    0,    0,  482,    0,    0,    0, -225,
    0,    0,    0,    0,    0,    0,   72,    0,    0,   76,
    0,   91,    0,    0,    0,    0,    0,    0,    0,    0,
   92,    0,   93,   94,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 -139,    0,    0,    0,    0,    0,    0,    0,    0,   98,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  101,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   99,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,
  };
  protected static  short [] yyGindex = {            0,
   -3,    0,  -98, -172,    0,  212,  242,  243,  241,  244,
   38,   66, -135,  -67, -202,  357,  215,  409,    0,   96,
    0,    0,  412, -193,    0,  126,    0,   97, -324, -301,
    0,  300,    0, -145,    0, -226,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  197,    0,  388,  193,  210,    0,    0,   88,
    0,    0,   89,    0,    0,   85,   87,    0,    0,    0,
    0,    0,    0,    0,    6,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   60,    0,    0,   25,
  };
  protected static  short [] yyTable = {           237,
   11,  316,  287,  262,  310,  158,  242,  345,   60,  397,
   43,  290,  299,  101,  200,  377,  201,  105,  296,   60,
  127,  372,  373,  330,   56,  176,  177,  114,  374,  130,
  220,  222,  300,  137,  223,  140,   44,   44,  378,  428,
  429,  193,  194,  346,  151,  324,  374,  224,  132,   70,
   46,   47,    6,  304,  276,  277,  278,  419,  133,  244,
  230,  246,  247,  248,  249,  250,  251,  252,  253,  254,
  255,  256,  257,  258,  259,  260,  261,  398,    8,    9,
  343,  377,  344,  331,  375,  114,   56,  114,  114,  114,
  114,  222,  159,  432,  223,  311,  112,   25,   26,   27,
   28,   29,  430,  376,  378,  234,  220,  224,  127,   70,
  103,  305,  342,  220,  114,  420,  433,  255,  230,  136,
  110,  431,  110,  110,  110,  279,  280,  287,  135,  392,
  138,  202,  141,  203,  204,  142,  290,  143,  232,  233,
  220,  367,  368,  296,  146,  205,  206,  207,  334,  243,
  147,  383,   54,  350,  351,  245,  365,  182,  183,  184,
  185,  153,  157,  234,  411,  186,  187,  188,  189,  178,
  180,   55,   56,  179,  181,  255,  208,  195,  196,  197,
  198,  363,  139,  139,  139,  139,  139,  139,  139,  139,
  139,  139,  139,  139,  139,  139,  139,  139,  139,  139,
  139,  139,  139,  292,  209,  220,  210,  220,  139,  442,
  211,  213,  202,  422,  203,  204,  214,  218,  399,  268,
  269,  270,  271,  190,  191,  192,  205,  206,  207,  219,
  227,  220,  321,  229,  323,  231,  220,  220,  220,  230,
  139,  422,  139,  139,  139,  139,  220,  236,  414,  239,
  416,  272,  273,  274,  275,  240,   11,  306,  241,  318,
   32,  326,  319,  328,  322,   11,  329,  332,  333,  139,
  335,  336,  337,  339,  347,  348,  349,  359,   11,  360,
  437,  362,  370,  382,  386,  387,  388,  393,  351,  400,
  396,   11,   11,  403,   11,   11,   11,   11,   11,   11,
   11,   11,   11,   11,   11,   11,   11,   11,  220,   11,
   11,   11,   11,   11,   11,  357,   11,  350,  404,   11,
   11,   11,   11,   11,   11,   11,   11,  123,  405,   11,
  406,  418,  409,  408,   11,   11,  220,  410,   11,  415,
  417,   11,   11,   11,   11,   11,   11,  425,  426,   11,
   11,  435,  438,  439,   11,  440,  395,   11,   11,   11,
   11,    3,   62,  104,   52,   11,  107,  108,  109,  110,
  111,   53,  115,  116,  117,  118,  119,  120,  121,  122,
   88,   66,  239,   67,   11,   11,  240,   47,  263,  412,
  243,  160,  161,  162,  163,  164,  165,  166,  167,  168,
  169,  170,  171,  172,  173,  174,    5,  154,  155,  156,
   89,   85,  244,   76,   77,    6,  175,   49,   91,  264,
  266,  265,  293,  113,  267,  366,  114,  199,    7,  341,
  225,  320,  325,  369,  150,  309,  384,  385,  390,  389,
  441,    8,    9,  434,   10,   11,   12,   13,   14,   15,
   16,   17,   18,   19,   20,   21,   22,   23,    0,   24,
   25,   26,   27,   28,   29,  421,   30,    0,    0,   31,
   32,  308,   33,   34,   35,   36,   37,    0,    0,   38,
  154,  155,  156,    0,   39,   40,    0,    0,   41,    0,
    0,   42,   43,   44,   45,   46,   47,    0,    0,   48,
   49,    0,    0,    0,    0,    0,    0,   50,   51,   52,
   53,    0,    0,    0,    0,   54,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   55,   56,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    5,    0,    0,    0,    0,
    0,  281,  282,  283,    6,    0,    0,  284,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    7,    0,    0,
    0,  301,  302,  303,    0,    0,    0,    0,    0,    0,
    8,    9,    0,   10,   11,   12,   13,   14,   15,   16,
   17,   18,   19,   20,   21,   22,   23,    0,   24,   25,
   26,   27,   28,   29,    0,   30,    0,    0,   31,   32,
    0,   33,   34,   35,   36,   37,    0,    0,   38,    0,
    0,    0,    0,   39,   40,    0,    0,   41,    0,    0,
   42,   43,   44,   45,   46,   47,    0,    0,   48,   49,
    0,  107,    0,  199,    0,    0,   50,   51,   52,   53,
    6,    0,    0,    0,   54,    0,    0,    0,    0,    0,
    0,    0,    0,    7,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   55,   56,    0,    8,    9,    0,   10,
   11,   12,   13,   14,   15,   16,   17,   18,   19,   20,
   21,   22,   23,    0,   24,   25,   26,   27,   28,   29,
    0,   30,    0,    0,   31,   32,    0,   33,   34,   35,
   36,   37,    0,    0,   38,    0,    0,    0,    0,   39,
   40,    0,    0,   41,    0,    0,   42,   43,   44,   45,
   46,   47,    0,    0,   48,   49,    0,    0,    0,    0,
    0,    0,   50,   51,   52,   53,  105,    0,    0,    0,
   54,    0,    0,    0,    0,    0,    0,    0,    0,  105,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   55,
   56,    0,  105,  105,    0,  105,  105,  105,  105,  105,
  105,  105,  105,  105,  105,  105,  105,  105,  105,    0,
    0,  105,  105,  105,  105,  105,    0,  105,    0,    0,
  105,  105,    0,  105,  105,  105,  105,  105,    0,    0,
  105,    0,    0,    0,    0,  105,  105,    0,    0,  105,
    0,    0,  105,  105,  105,  105,  105,  105,    0,    0,
  105,  105,    0,    0,    0,    0,    0,    0,  105,  105,
  105,  105,    6,    0,    0,    0,  105,    0,    0,    0,
    0,    0,    0,    0,    0,    7,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  105,  105,    0,    8,    9,
    0,   10,   11,   12,   13,   14,   15,   16,   17,   18,
   19,   20,   21,   22,   23,    0,   24,   25,   26,   27,
   28,   29,    0,    0,    0,    0,    0,    0,    0,    0,
  103,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  123,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  124,  125,  126,    6,    0,
    0,    0,   54,    0,    0,    0,    0,    0,    0,    0,
    0,    7,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   55,   56,    0,    8,    9,    0,   10,   11,   12,
   13,   14,   15,   16,   17,   18,   19,   20,   21,   22,
   23,    6,   24,   25,   26,   27,   28,   29,    0,    0,
    0,    0,  139,    0,    7,    0,  103,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    8,    9,    0,
   10,   11,   12,   13,   14,   15,   16,   17,   18,   19,
   20,   21,   22,   23,    0,   24,   25,   26,   27,   28,
   29,   51,   52,   53,    0,    0,    0,    0,   54,  103,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   55,   56,    0,
    0,    0,  123,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   51,   52,   53,    6,    0,    0,
    0,   54,    0,    0,    0,    0,    0,    0,    0,    0,
    7,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   55,   56,    0,    8,    9,    0,   10,   11,   12,   13,
   14,   15,   16,   17,   18,   19,   20,   21,   22,   23,
    0,   24,   25,   26,   27,   28,   29,    0,    0,  215,
    0,    0,    0,    0,    0,  103,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   51,   52,   53,    6,    0,    0,    0,   54,    0,    0,
    0,    0,    0,    0,    0,    0,    7,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   55,   56,    0,    8,
    9,    0,   10,   11,   12,   13,   14,   15,   16,   17,
   18,   19,   20,   21,   22,   23,    0,   24,   25,   26,
   27,   28,   29,    0,    0,  216,    0,    0,    0,    0,
    0,  103,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   51,   52,   53,    6,
    0,    0,    0,   54,    0,    0,    0,    0,    0,    0,
    0,    0,    7,    0,    0,    0,    0,    0,    0,    0,
    0,    0,   55,   56,    0,    8,    9,    0,   10,   11,
   12,   13,   14,   15,   16,   17,   18,   19,   20,   21,
   22,   23,    6,   24,   25,   26,   27,   28,   29,    0,
    0,  217,    0,    0,    0,    7,    0,  103,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    8,    9,
    0,  285,   11,   12,   13,   14,   15,   16,   17,   18,
   19,   20,   21,   22,   23,    0,   24,   25,   26,   27,
   28,   29,   51,   52,   53,    0,    0,    0,    0,   54,
  103,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   55,   56,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  286,    0,    0,   51,   52,   53,    6,    0,
    0,    0,   54,    0,    0,    0,    0,    0,    0,    0,
    0,    7,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   55,   56,    0,    8,    9,    0,   10,   11,   12,
   13,   14,   15,   16,   17,   18,   19,   20,   21,   22,
   23,    6,   24,   25,   26,   27,   28,   29,    0,    0,
    0,    0,    0,    0,    7,    0,  103,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    8,    9,    0,
   10,   11,   12,   13,   14,   15,   16,   17,   18,   19,
   20,   21,   22,   23,    0,   24,   25,   26,   27,   28,
   29,   51,   52,   53,    0,    0,    0,    0,   54,  103,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   55,   56,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   51,   52,   53,    6,    0,    0,
    0,   54,    0,    0,    0,    0,    0,    0,    0,    0,
    7,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   55,  295,    0,    8,    9,    0,  285,   11,   12,   13,
   14,   15,   16,   17,   18,   19,   20,   21,   22,   23,
    0,   24,   25,   26,   27,   28,   29,    0,    0,    0,
    0,    0,    0,    0,    0,  103,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   51,   52,   53,    0,    0,    0,    0,   54,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   55,   56,  171,  171,
  171,  171,  171,  171,  171,  171,  171,  171,  171,  171,
  171,  171,  171,  171,  171,  171,  171,  171,  171,  171,
  171,  171,  171,  171,  171,  171,  171,  171,  171,  171,
  171,  171,  171,  171,  171,  171,  195,    0,  195,  195,
    0,    0,    0,  171,  171,    0,    0,    0,    0,    0,
  195,  195,  195,    0,    0,    0,  171,    0,  171,  171,
  171,  171,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  171,  190,  190,  190,  190,
  190,  190,  190,  190,  190,  190,  190,  190,  190,  190,
  190,  190,  190,  190,  190,  190,  190,  190,  190,  190,
  190,  190,  190,  190,  190,  190,  190,  190,  190,  190,
  190,  190,  190,  190,    0,    0,    0,    0,    0,    0,
    0,  190,  190,    0,    0,  190,    0,  190,    0,    0,
    0,    0,    0,    0,  190,    0,  190,  190,  190,  190,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  190,  166,  166,  166,  166,  166,  166,
  166,  166,  166,  166,  166,  166,  166,  166,  166,  166,
  166,  166,  166,  166,  166,  166,  166,  166,  166,  166,
  166,  166,  166,  166,  166,  166,  166,  166,  166,  166,
  166,  166,    0,    0,    0,    0,    0,    0,    0,  166,
  166,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  166,    0,  166,  166,  166,  166,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  166,  157,  157,  157,  157,  157,  157,  157,  157,
  157,  157,  157,  157,  157,  157,  157,  157,  157,  157,
  157,  157,  157,  157,  157,  157,  157,  157,  157,  157,
  157,  157,  157,  157,  157,  157,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  157,  157,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  157,    0,  157,  157,  157,  157,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  157,
  158,  158,  158,  158,  158,  158,  158,  158,  158,  158,
  158,  158,  158,  158,  158,  158,  158,  158,  158,  158,
  158,  158,  158,  158,  158,  158,  158,  158,  158,  158,
  158,  158,  158,  158,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  158,  158,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  158,    0,
  158,  158,  158,  158,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  158,  159,  159,
  159,  159,  159,  159,  159,  159,  159,  159,  159,  159,
  159,  159,  159,  159,  159,  159,  159,  159,  159,  159,
  159,  159,  159,  159,  159,  159,  159,  159,  159,  159,
  159,  159,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  159,  159,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  159,    0,  159,  159,
  159,  159,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  159,  153,  153,  153,  153,
  153,  153,  153,  153,  153,  153,  153,  153,  153,  153,
  153,  153,  153,  153,  153,  153,  153,  153,  153,  153,
  153,  153,  153,  153,  153,  153,  153,  153,  153,  153,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  153,    0,  153,  153,  153,  153,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  153,  154,  154,  154,  154,  154,  154,
  154,  154,  154,  154,  154,  154,  154,  154,  154,  154,
  154,  154,  154,  154,  154,  154,  154,  154,  154,  154,
  154,  154,  154,  154,  154,  154,  154,  154,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  154,    0,  154,  154,  154,  154,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  154,  155,  155,  155,  155,  155,  155,  155,  155,
  155,  155,  155,  155,  155,  155,  155,  155,  155,  155,
  155,  155,  155,  155,  155,  155,  155,  155,  155,  155,
  155,  155,  155,  155,  155,  155,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  155,    0,  155,  155,  155,  155,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  155,
  156,  156,  156,  156,  156,  156,  156,  156,  156,  156,
  156,  156,  156,  156,  156,  156,  156,  156,  156,  156,
  156,  156,  156,  156,  156,  156,  156,  156,  156,  156,
  156,  156,  156,  156,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  156,    0,
  156,  156,  156,  156,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  156,  148,  148,
  148,  148,  148,  148,  148,  148,  148,  148,  148,  148,
  148,  148,  148,  148,  148,  148,  148,  148,  148,  148,
  148,  148,  148,  148,  148,  148,  148,  148,  148,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  148,    0,  148,  148,
  148,  148,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  148,  149,  149,  149,  149,
  149,  149,  149,  149,  149,  149,  149,  149,  149,  149,
  149,  149,  149,  149,  149,  149,  149,  149,  149,  149,
  149,  149,  149,  149,  149,  149,  149,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  149,    0,  149,  149,  149,  149,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  149,  150,  150,  150,  150,  150,  150,
  150,  150,  150,  150,  150,  150,  150,  150,  150,  150,
  150,  150,  150,  150,  150,  150,  150,  150,  150,  150,
  150,  150,  150,  150,  150,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  150,    0,  150,  150,  150,  150,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  150,  151,  151,  151,  151,  151,  151,  151,  151,
  151,  151,  151,  151,  151,  151,  151,  151,  151,  151,
  151,  151,  151,  151,  151,  151,  151,  151,  151,  151,
  151,  151,  151,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  151,    0,  151,  151,  151,  151,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  151,
  152,  152,  152,  152,  152,  152,  152,  152,  152,  152,
  152,  152,  152,  152,  152,  152,  152,  152,  152,  152,
  152,  152,  152,  152,  152,  152,  152,  152,  152,  152,
  152,  143,  143,  143,  143,  143,  143,  143,  143,  143,
  143,  143,  143,  143,  143,  143,  143,  143,  143,  143,
  143,  143,  143,  143,  143,  143,  143,  143,  152,    0,
  152,  152,  152,  152,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  152,    0,  143,
    0,  143,  143,  143,  143,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  143,  144,
  144,  144,  144,  144,  144,  144,  144,  144,  144,  144,
  144,  144,  144,  144,  144,  144,  144,  144,  144,  144,
  144,  144,  144,  144,  144,  144,    0,  145,  145,  145,
  145,  145,  145,  145,  145,  145,  145,  145,  145,  145,
  145,  145,  145,  145,  145,  145,  145,  145,  145,  145,
  145,  145,  145,  145,    0,    0,    0,  144,    0,  144,
  144,  144,  144,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  145,  144,  145,  145,  145,
  145,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  145,  146,  146,  146,  146,  146,
  146,  146,  146,  146,  146,  146,  146,  146,  146,  146,
  146,  146,  146,  146,  146,  146,  146,  146,  146,  146,
  146,  146,    0,  147,  147,  147,  147,  147,  147,  147,
  147,  147,  147,  147,  147,  147,  147,  147,  147,  147,
  147,  147,  147,  147,  147,  147,  147,  147,  147,  147,
    0,    0,    0,  146,    0,  146,  146,  146,  146,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  147,  146,  147,  147,  147,  147,  141,  141,  141,
  141,  141,  141,  141,  141,  141,  141,  141,  141,  141,
  141,  141,  141,  141,  141,  141,  141,  141,  141,    0,
  147,    0,    0,  141,    0,  142,  142,  142,  142,  142,
  142,  142,  142,  142,  142,  142,  142,  142,  142,  142,
  142,  142,  142,  142,  142,  142,  142,    0,    0,    0,
    0,  142,    0,    0,    0,  141,    0,  141,  141,  141,
  141,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  142,  141,  142,  142,  142,  142,  140,
  140,  140,  140,  140,  140,  140,  140,  140,  140,  140,
  140,  140,  140,  140,  140,  140,  140,  140,  140,  140,
    0,    0,  142,    0,    0,  140,    0,  137,  137,  137,
  137,  137,  137,  137,  137,  137,  137,  137,  137,  137,
  137,  137,  137,  137,  137,  137,  137,    0,    0,    0,
    0,    0,    0,  137,    0,    0,    0,  140,    0,  140,
  140,  140,  140,    0,    0,  138,  138,  138,  138,  138,
  138,  138,  138,  138,  138,  138,  138,  138,  138,  138,
  138,  138,  138,  138,  138,  137,  140,  137,  137,  137,
  137,  138,    0,  135,  135,  135,  135,  135,  135,  135,
  135,  135,  135,  135,  135,  135,  135,  135,  135,  135,
  135,  135,    0,    0,  137,    0,    0,    0,    0,  135,
    0,    0,    0,  138,    0,  138,  138,  138,  138,    0,
    0,  136,  136,  136,  136,  136,  136,  136,  136,  136,
  136,  136,  136,  136,  136,  136,  136,  136,  136,  136,
    0,  135,  138,  135,  135,  135,  135,  136,    0,  133,
  133,  133,  133,  133,  133,  133,  133,  133,  133,  133,
  133,  133,  133,  133,  133,  133,  133,    0,    0,    0,
  135,    0,    0,    0,    0,  133,    0,    0,    0,  136,
    0,  136,  136,  136,  136,    0,    0,  134,  134,  134,
  134,  134,  134,  134,  134,  134,  134,  134,  134,  134,
  134,  134,  134,  134,  134,    0,    0,  133,  136,  133,
  133,  133,  133,  134,    0,  131,  131,  131,  131,  131,
  131,  131,  131,  131,  131,  131,  131,  131,  131,  131,
  131,    0,    0,    0,    0,    0,  133,    0,    0,    0,
    0,  131,    0,    0,    0,  134,    0,  134,  134,  134,
  134,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  131,  134,  131,  131,  131,  131,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  131,  204,  204,  204,  204,  204,  204,  204,
  204,  204,  204,  204,  204,  204,  204,  204,  204,  204,
  204,  204,  204,  204,  204,  204,  204,  204,  204,  204,
  204,  204,  204,  204,  204,  204,  204,  204,  204,  204,
  204,  204,    0,  204,  204,    0,    0,    0,  204,  204,
    0,    0,  204,    0,  204,  204,  204,  204,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  204,
  };
  protected static  short [] yyCheck = {           145,
    0,  228,  205,  176,  294,  257,  152,  258,  309,  317,
  319,  205,  291,  319,  306,  340,  308,  311,  212,  320,
   24,  302,  303,  257,  257,  273,  274,  257,  309,  385,
  129,  257,  311,   37,  257,   39,  345,  346,  340,  302,
  303,  302,  303,  294,   48,  239,  309,  257,  319,  257,
  345,  346,  265,  257,  190,  191,  192,  257,  385,  158,
  257,  160,  161,  162,  163,  164,  165,  166,  167,  168,
  169,  170,  171,  172,  173,  174,  175,  385,  291,  292,
  307,  406,  309,  317,  365,  315,  319,  317,  318,  319,
  320,  317,  344,  418,  317,  385,  309,  310,  311,  312,
  313,  314,  365,  384,  406,  257,  205,  317,  112,  317,
  323,  315,  306,  212,  344,  315,  418,  257,  315,  318,
  315,  384,  317,  318,  319,  193,  194,  330,  319,  356,
  385,  295,  319,  297,  298,  309,  330,  309,  142,  143,
  239,  335,  336,  337,  309,  309,  310,  311,  294,  153,
  385,  345,  365,  351,  352,  159,  329,  279,  280,  281,
  282,  309,  319,  315,  391,  284,  285,  286,  287,  275,
  277,  384,  385,  276,  278,  315,  340,  291,  292,  293,
  294,  327,  257,  258,  259,  260,  261,  262,  263,  264,
  265,  266,  267,  268,  269,  270,  271,  272,  273,  274,
  275,  276,  277,  207,  353,  304,  319,  306,  283,  436,
  386,  386,  295,  407,  297,  298,  317,  317,  364,  182,
  183,  184,  185,  288,  289,  290,  309,  310,  311,  385,
  309,  330,  236,  318,  238,  319,  335,  336,  337,  320,
  315,  435,  317,  318,  319,  320,  345,  309,  394,  258,
  396,  186,  187,  188,  189,  257,  256,  327,  319,  317,
  320,  354,  319,  317,  342,  265,  318,  385,  315,  344,
  318,  257,  257,  311,  317,  317,  257,  317,  278,  309,
  426,  309,  315,  257,  309,  309,  321,  319,  352,  384,
  317,  291,  292,  384,  294,  295,  296,  297,  298,  299,
  300,  301,  302,  303,  304,  305,  306,  307,  407,  309,
  310,  311,  312,  313,  314,  319,  316,  351,  384,  319,
  320,  321,  322,  323,  324,  325,  326,  346,  315,  329,
  257,  257,  385,  317,  334,  335,  435,  317,  338,  317,
  317,  341,  342,  343,  344,  345,  346,  317,  317,  349,
  350,  257,  319,  384,  354,  384,  360,  357,  358,  359,
  360,    0,  320,    7,  319,  365,   10,   11,   12,   13,
   14,  319,   16,   17,   18,   19,   20,   21,   22,   23,
  320,  317,  315,  317,  384,  385,  315,  319,  177,  393,
  315,  258,  259,  260,  261,  262,  263,  264,  265,  266,
  267,  268,  269,  270,  271,  272,  256,   51,   52,   53,
  320,  320,  315,  321,  321,  265,  283,  317,  320,  178,
  180,  179,  208,   15,  181,  330,   15,   71,  278,  304,
  131,  235,  240,  337,   47,  226,  349,  349,  354,  353,
  435,  291,  292,  419,  294,  295,  296,  297,  298,  299,
  300,  301,  302,  303,  304,  305,  306,  307,   -1,  309,
  310,  311,  312,  313,  314,  406,  316,   -1,   -1,  319,
  320,  321,  322,  323,  324,  325,  326,   -1,   -1,  329,
  124,  125,  126,   -1,  334,  335,   -1,   -1,  338,   -1,
   -1,  341,  342,  343,  344,  345,  346,   -1,   -1,  349,
  350,   -1,   -1,   -1,   -1,   -1,   -1,  357,  358,  359,
  360,   -1,   -1,   -1,   -1,  365,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  384,  385,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  256,   -1,   -1,   -1,   -1,
   -1,  195,  196,  197,  265,   -1,   -1,  201,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  278,   -1,   -1,
   -1,  215,  216,  217,   -1,   -1,   -1,   -1,   -1,   -1,
  291,  292,   -1,  294,  295,  296,  297,  298,  299,  300,
  301,  302,  303,  304,  305,  306,  307,   -1,  309,  310,
  311,  312,  313,  314,   -1,  316,   -1,   -1,  319,  320,
   -1,  322,  323,  324,  325,  326,   -1,   -1,  329,   -1,
   -1,   -1,   -1,  334,  335,   -1,   -1,  338,   -1,   -1,
  341,  342,  343,  344,  345,  346,   -1,   -1,  349,  350,
   -1,  285,   -1,  287,   -1,   -1,  357,  358,  359,  360,
  265,   -1,   -1,   -1,  365,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  278,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  384,  385,   -1,  291,  292,   -1,  294,
  295,  296,  297,  298,  299,  300,  301,  302,  303,  304,
  305,  306,  307,   -1,  309,  310,  311,  312,  313,  314,
   -1,  316,   -1,   -1,  319,  320,   -1,  322,  323,  324,
  325,  326,   -1,   -1,  329,   -1,   -1,   -1,   -1,  334,
  335,   -1,   -1,  338,   -1,   -1,  341,  342,  343,  344,
  345,  346,   -1,   -1,  349,  350,   -1,   -1,   -1,   -1,
   -1,   -1,  357,  358,  359,  360,  265,   -1,   -1,   -1,
  365,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  278,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  384,
  385,   -1,  291,  292,   -1,  294,  295,  296,  297,  298,
  299,  300,  301,  302,  303,  304,  305,  306,  307,   -1,
   -1,  310,  311,  312,  313,  314,   -1,  316,   -1,   -1,
  319,  320,   -1,  322,  323,  324,  325,  326,   -1,   -1,
  329,   -1,   -1,   -1,   -1,  334,  335,   -1,   -1,  338,
   -1,   -1,  341,  342,  343,  344,  345,  346,   -1,   -1,
  349,  350,   -1,   -1,   -1,   -1,   -1,   -1,  357,  358,
  359,  360,  265,   -1,   -1,   -1,  365,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  278,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  384,  385,   -1,  291,  292,
   -1,  294,  295,  296,  297,  298,  299,  300,  301,  302,
  303,  304,  305,  306,  307,   -1,  309,  310,  311,  312,
  313,  314,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  323,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  346,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  358,  359,  360,  265,   -1,
   -1,   -1,  365,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  278,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  384,  385,   -1,  291,  292,   -1,  294,  295,  296,
  297,  298,  299,  300,  301,  302,  303,  304,  305,  306,
  307,  265,  309,  310,  311,  312,  313,  314,   -1,   -1,
   -1,   -1,  319,   -1,  278,   -1,  323,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  291,  292,   -1,
  294,  295,  296,  297,  298,  299,  300,  301,  302,  303,
  304,  305,  306,  307,   -1,  309,  310,  311,  312,  313,
  314,  358,  359,  360,   -1,   -1,   -1,   -1,  365,  323,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  384,  385,   -1,
   -1,   -1,  346,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  358,  359,  360,  265,   -1,   -1,
   -1,  365,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  278,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  384,  385,   -1,  291,  292,   -1,  294,  295,  296,  297,
  298,  299,  300,  301,  302,  303,  304,  305,  306,  307,
   -1,  309,  310,  311,  312,  313,  314,   -1,   -1,  317,
   -1,   -1,   -1,   -1,   -1,  323,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  358,  359,  360,  265,   -1,   -1,   -1,  365,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  278,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  384,  385,   -1,  291,
  292,   -1,  294,  295,  296,  297,  298,  299,  300,  301,
  302,  303,  304,  305,  306,  307,   -1,  309,  310,  311,
  312,  313,  314,   -1,   -1,  317,   -1,   -1,   -1,   -1,
   -1,  323,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  358,  359,  360,  265,
   -1,   -1,   -1,  365,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  278,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  384,  385,   -1,  291,  292,   -1,  294,  295,
  296,  297,  298,  299,  300,  301,  302,  303,  304,  305,
  306,  307,  265,  309,  310,  311,  312,  313,  314,   -1,
   -1,  317,   -1,   -1,   -1,  278,   -1,  323,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  291,  292,
   -1,  294,  295,  296,  297,  298,  299,  300,  301,  302,
  303,  304,  305,  306,  307,   -1,  309,  310,  311,  312,
  313,  314,  358,  359,  360,   -1,   -1,   -1,   -1,  365,
  323,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  384,  385,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  355,   -1,   -1,  358,  359,  360,  265,   -1,
   -1,   -1,  365,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  278,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  384,  385,   -1,  291,  292,   -1,  294,  295,  296,
  297,  298,  299,  300,  301,  302,  303,  304,  305,  306,
  307,  265,  309,  310,  311,  312,  313,  314,   -1,   -1,
   -1,   -1,   -1,   -1,  278,   -1,  323,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  291,  292,   -1,
  294,  295,  296,  297,  298,  299,  300,  301,  302,  303,
  304,  305,  306,  307,   -1,  309,  310,  311,  312,  313,
  314,  358,  359,  360,   -1,   -1,   -1,   -1,  365,  323,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  384,  385,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  358,  359,  360,  265,   -1,   -1,
   -1,  365,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  278,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  384,  385,   -1,  291,  292,   -1,  294,  295,  296,  297,
  298,  299,  300,  301,  302,  303,  304,  305,  306,  307,
   -1,  309,  310,  311,  312,  313,  314,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  323,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  358,  359,  360,   -1,   -1,   -1,   -1,  365,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  384,  385,  257,  258,
  259,  260,  261,  262,  263,  264,  265,  266,  267,  268,
  269,  270,  271,  272,  273,  274,  275,  276,  277,  278,
  279,  280,  281,  282,  283,  284,  285,  286,  287,  288,
  289,  290,  291,  292,  293,  294,  295,   -1,  297,  298,
   -1,   -1,   -1,  302,  303,   -1,   -1,   -1,   -1,   -1,
  309,  310,  311,   -1,   -1,   -1,  315,   -1,  317,  318,
  319,  320,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  344,  257,  258,  259,  260,
  261,  262,  263,  264,  265,  266,  267,  268,  269,  270,
  271,  272,  273,  274,  275,  276,  277,  278,  279,  280,
  281,  282,  283,  284,  285,  286,  287,  288,  289,  290,
  291,  292,  293,  294,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  302,  303,   -1,   -1,  306,   -1,  308,   -1,   -1,
   -1,   -1,   -1,   -1,  315,   -1,  317,  318,  319,  320,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  344,  257,  258,  259,  260,  261,  262,
  263,  264,  265,  266,  267,  268,  269,  270,  271,  272,
  273,  274,  275,  276,  277,  278,  279,  280,  281,  282,
  283,  284,  285,  286,  287,  288,  289,  290,  291,  292,
  293,  294,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  302,
  303,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  315,   -1,  317,  318,  319,  320,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  344,  257,  258,  259,  260,  261,  262,  263,  264,
  265,  266,  267,  268,  269,  270,  271,  272,  273,  274,
  275,  276,  277,  278,  279,  280,  281,  282,  283,  284,
  285,  286,  287,  288,  289,  290,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  302,  303,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  315,   -1,  317,  318,  319,  320,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  344,
  257,  258,  259,  260,  261,  262,  263,  264,  265,  266,
  267,  268,  269,  270,  271,  272,  273,  274,  275,  276,
  277,  278,  279,  280,  281,  282,  283,  284,  285,  286,
  287,  288,  289,  290,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  302,  303,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  315,   -1,
  317,  318,  319,  320,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  344,  257,  258,
  259,  260,  261,  262,  263,  264,  265,  266,  267,  268,
  269,  270,  271,  272,  273,  274,  275,  276,  277,  278,
  279,  280,  281,  282,  283,  284,  285,  286,  287,  288,
  289,  290,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  302,  303,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  315,   -1,  317,  318,
  319,  320,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  344,  257,  258,  259,  260,
  261,  262,  263,  264,  265,  266,  267,  268,  269,  270,
  271,  272,  273,  274,  275,  276,  277,  278,  279,  280,
  281,  282,  283,  284,  285,  286,  287,  288,  289,  290,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  315,   -1,  317,  318,  319,  320,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  344,  257,  258,  259,  260,  261,  262,
  263,  264,  265,  266,  267,  268,  269,  270,  271,  272,
  273,  274,  275,  276,  277,  278,  279,  280,  281,  282,
  283,  284,  285,  286,  287,  288,  289,  290,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  315,   -1,  317,  318,  319,  320,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  344,  257,  258,  259,  260,  261,  262,  263,  264,
  265,  266,  267,  268,  269,  270,  271,  272,  273,  274,
  275,  276,  277,  278,  279,  280,  281,  282,  283,  284,
  285,  286,  287,  288,  289,  290,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  315,   -1,  317,  318,  319,  320,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  344,
  257,  258,  259,  260,  261,  262,  263,  264,  265,  266,
  267,  268,  269,  270,  271,  272,  273,  274,  275,  276,
  277,  278,  279,  280,  281,  282,  283,  284,  285,  286,
  287,  288,  289,  290,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  315,   -1,
  317,  318,  319,  320,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  344,  257,  258,
  259,  260,  261,  262,  263,  264,  265,  266,  267,  268,
  269,  270,  271,  272,  273,  274,  275,  276,  277,  278,
  279,  280,  281,  282,  283,  284,  285,  286,  287,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  315,   -1,  317,  318,
  319,  320,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  344,  257,  258,  259,  260,
  261,  262,  263,  264,  265,  266,  267,  268,  269,  270,
  271,  272,  273,  274,  275,  276,  277,  278,  279,  280,
  281,  282,  283,  284,  285,  286,  287,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  315,   -1,  317,  318,  319,  320,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  344,  257,  258,  259,  260,  261,  262,
  263,  264,  265,  266,  267,  268,  269,  270,  271,  272,
  273,  274,  275,  276,  277,  278,  279,  280,  281,  282,
  283,  284,  285,  286,  287,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  315,   -1,  317,  318,  319,  320,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  344,  257,  258,  259,  260,  261,  262,  263,  264,
  265,  266,  267,  268,  269,  270,  271,  272,  273,  274,
  275,  276,  277,  278,  279,  280,  281,  282,  283,  284,
  285,  286,  287,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  315,   -1,  317,  318,  319,  320,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  344,
  257,  258,  259,  260,  261,  262,  263,  264,  265,  266,
  267,  268,  269,  270,  271,  272,  273,  274,  275,  276,
  277,  278,  279,  280,  281,  282,  283,  284,  285,  286,
  287,  257,  258,  259,  260,  261,  262,  263,  264,  265,
  266,  267,  268,  269,  270,  271,  272,  273,  274,  275,
  276,  277,  278,  279,  280,  281,  282,  283,  315,   -1,
  317,  318,  319,  320,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  344,   -1,  315,
   -1,  317,  318,  319,  320,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  344,  257,
  258,  259,  260,  261,  262,  263,  264,  265,  266,  267,
  268,  269,  270,  271,  272,  273,  274,  275,  276,  277,
  278,  279,  280,  281,  282,  283,   -1,  257,  258,  259,
  260,  261,  262,  263,  264,  265,  266,  267,  268,  269,
  270,  271,  272,  273,  274,  275,  276,  277,  278,  279,
  280,  281,  282,  283,   -1,   -1,   -1,  315,   -1,  317,
  318,  319,  320,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  315,  344,  317,  318,  319,
  320,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  344,  257,  258,  259,  260,  261,
  262,  263,  264,  265,  266,  267,  268,  269,  270,  271,
  272,  273,  274,  275,  276,  277,  278,  279,  280,  281,
  282,  283,   -1,  257,  258,  259,  260,  261,  262,  263,
  264,  265,  266,  267,  268,  269,  270,  271,  272,  273,
  274,  275,  276,  277,  278,  279,  280,  281,  282,  283,
   -1,   -1,   -1,  315,   -1,  317,  318,  319,  320,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  315,  344,  317,  318,  319,  320,  257,  258,  259,
  260,  261,  262,  263,  264,  265,  266,  267,  268,  269,
  270,  271,  272,  273,  274,  275,  276,  277,  278,   -1,
  344,   -1,   -1,  283,   -1,  257,  258,  259,  260,  261,
  262,  263,  264,  265,  266,  267,  268,  269,  270,  271,
  272,  273,  274,  275,  276,  277,  278,   -1,   -1,   -1,
   -1,  283,   -1,   -1,   -1,  315,   -1,  317,  318,  319,
  320,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  315,  344,  317,  318,  319,  320,  257,
  258,  259,  260,  261,  262,  263,  264,  265,  266,  267,
  268,  269,  270,  271,  272,  273,  274,  275,  276,  277,
   -1,   -1,  344,   -1,   -1,  283,   -1,  257,  258,  259,
  260,  261,  262,  263,  264,  265,  266,  267,  268,  269,
  270,  271,  272,  273,  274,  275,  276,   -1,   -1,   -1,
   -1,   -1,   -1,  283,   -1,   -1,   -1,  315,   -1,  317,
  318,  319,  320,   -1,   -1,  257,  258,  259,  260,  261,
  262,  263,  264,  265,  266,  267,  268,  269,  270,  271,
  272,  273,  274,  275,  276,  315,  344,  317,  318,  319,
  320,  283,   -1,  257,  258,  259,  260,  261,  262,  263,
  264,  265,  266,  267,  268,  269,  270,  271,  272,  273,
  274,  275,   -1,   -1,  344,   -1,   -1,   -1,   -1,  283,
   -1,   -1,   -1,  315,   -1,  317,  318,  319,  320,   -1,
   -1,  257,  258,  259,  260,  261,  262,  263,  264,  265,
  266,  267,  268,  269,  270,  271,  272,  273,  274,  275,
   -1,  315,  344,  317,  318,  319,  320,  283,   -1,  257,
  258,  259,  260,  261,  262,  263,  264,  265,  266,  267,
  268,  269,  270,  271,  272,  273,  274,   -1,   -1,   -1,
  344,   -1,   -1,   -1,   -1,  283,   -1,   -1,   -1,  315,
   -1,  317,  318,  319,  320,   -1,   -1,  257,  258,  259,
  260,  261,  262,  263,  264,  265,  266,  267,  268,  269,
  270,  271,  272,  273,  274,   -1,   -1,  315,  344,  317,
  318,  319,  320,  283,   -1,  257,  258,  259,  260,  261,
  262,  263,  264,  265,  266,  267,  268,  269,  270,  271,
  272,   -1,   -1,   -1,   -1,   -1,  344,   -1,   -1,   -1,
   -1,  283,   -1,   -1,   -1,  315,   -1,  317,  318,  319,
  320,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  315,  344,  317,  318,  319,  320,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  344,  257,  258,  259,  260,  261,  262,  263,
  264,  265,  266,  267,  268,  269,  270,  271,  272,  273,
  274,  275,  276,  277,  278,  279,  280,  281,  282,  283,
  284,  285,  286,  287,  288,  289,  290,  291,  292,  293,
  294,  295,   -1,  297,  298,   -1,   -1,   -1,  302,  303,
   -1,   -1,  306,   -1,  308,  309,  310,  311,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  340,
  };

#line 789 "KagexEnvinitParser.jay"
	}	//class KagexEnvinitParser を閉じる

#line default
namespace yydebug {
        using System;
	 internal interface yyDebug {
		 void push (int state, Object value);
		 void lex (int state, int token, string name, Object value);
		 void shift (int from, int to, int errorFlag);
		 void pop (int state);
		 void discard (int state, int token, string name, Object value);
		 void reduce (int from, int to, int rule, string text, int len);
		 void shift (int from, int to);
		 void accept (Object value);
		 void error (string message);
		 void reject ();
	 }
	 
	 class yyDebugSimple : yyDebug {
		 void println (string s){
			 Console.Error.WriteLine (s);
		 }
		 
		 public void push (int state, Object value) {
			 println ("push\tstate "+state+"\tvalue "+value);
		 }
		 
		 public void lex (int state, int token, string name, Object value) {
			 println("lex\tstate "+state+"\treading "+name+"\tvalue "+value);
		 }
		 
		 public void shift (int from, int to, int errorFlag) {
			 switch (errorFlag) {
			 default:				// normally
				 println("shift\tfrom state "+from+" to "+to);
				 break;
			 case 0: case 1: case 2:		// in error recovery
				 println("shift\tfrom state "+from+" to "+to
					     +"\t"+errorFlag+" left to recover");
				 break;
			 case 3:				// normally
				 println("shift\tfrom state "+from+" to "+to+"\ton error");
				 break;
			 }
		 }
		 
		 public void pop (int state) {
			 println("pop\tstate "+state+"\ton error");
		 }
		 
		 public void discard (int state, int token, string name, Object value) {
			 println("discard\tstate "+state+"\ttoken "+name+"\tvalue "+value);
		 }
		 
		 public void reduce (int from, int to, int rule, string text, int len) {
			 println("reduce\tstate "+from+"\tuncover "+to
				     +"\trule ("+rule+") "+text);
		 }
		 
		 public void shift (int from, int to) {
			 println("goto\tfrom state "+from+" to "+to);
		 }
		 
		 public void accept (Object value) {
			 println("accept\tvalue "+value);
		 }
		 
		 public void error (string message) {
			 println("error\t"+message);
		 }
		 
		 public void reject () {
			 println("reject");
		 }
		 
	 }
}
// %token constants
 class Token {
  public const int T_COMMA = 257;
  public const int T_EQUAL = 258;
  public const int T_AMPERSANDEQUAL = 259;
  public const int T_VERTLINEEQUAL = 260;
  public const int T_CHEVRONEQUAL = 261;
  public const int T_MINUSEQUAL = 262;
  public const int T_PLUSEQUAL = 263;
  public const int T_PERCENTEQUAL = 264;
  public const int T_SLASHEQUAL = 265;
  public const int T_BACKSLASHEQUAL = 266;
  public const int T_ASTERISKEQUAL = 267;
  public const int T_LOGICALOREQUAL = 268;
  public const int T_LOGICALANDEQUAL = 269;
  public const int T_RARITHSHIFTEQUAL = 270;
  public const int T_LARITHSHIFTEQUAL = 271;
  public const int T_RBITSHIFTEQUAL = 272;
  public const int T_QUESTION = 273;
  public const int T_LOGICALOR = 274;
  public const int T_LOGICALAND = 275;
  public const int T_VERTLINE = 276;
  public const int T_CHEVRON = 277;
  public const int T_AMPERSAND = 278;
  public const int T_NOTEQUAL = 279;
  public const int T_EQUALEQUAL = 280;
  public const int T_DISCNOTEQUAL = 281;
  public const int T_DISCEQUAL = 282;
  public const int T_SWAP = 283;
  public const int T_LT = 284;
  public const int T_GT = 285;
  public const int T_LTOREQUAL = 286;
  public const int T_GTOREQUAL = 287;
  public const int T_RARITHSHIFT = 288;
  public const int T_LARITHSHIFT = 289;
  public const int T_RBITSHIFT = 290;
  public const int T_PERCENT = 291;
  public const int T_SLASH = 292;
  public const int T_BACKSLASH = 293;
  public const int T_ASTERISK = 294;
  public const int T_EXCRAMATION = 295;
  public const int T_TILDE = 296;
  public const int T_DECREMENT = 297;
  public const int T_INCREMENT = 298;
  public const int T_NEW = 299;
  public const int T_DELETE = 300;
  public const int T_TYPEOF = 301;
  public const int T_PLUS = 302;
  public const int T_MINUS = 303;
  public const int T_SHARP = 304;
  public const int T_DOLLAR = 305;
  public const int T_ISVALID = 306;
  public const int T_INVALIDATE = 307;
  public const int T_INSTANCEOF = 308;
  public const int T_LPARENTHESIS = 309;
  public const int T_DOT = 310;
  public const int T_LBRACKET = 311;
  public const int T_THIS = 312;
  public const int T_SUPER = 313;
  public const int T_GLOBAL = 314;
  public const int T_RBRACKET = 315;
  public const int T_CLASS = 316;
  public const int T_RPARENTHESIS = 317;
  public const int T_COLON = 318;
  public const int T_SEMICOLON = 319;
  public const int T_LBRACE = 320;
  public const int T_RBRACE = 321;
  public const int T_CONTINUE = 322;
  public const int T_FUNCTION = 323;
  public const int T_DEBUGGER = 324;
  public const int T_DEFAULT = 325;
  public const int T_CASE = 326;
  public const int T_EXTENDS = 327;
  public const int T_FINALLY = 328;
  public const int T_PROPERTY = 329;
  public const int T_PRIVATE = 330;
  public const int T_PUBLIC = 331;
  public const int T_PROTECTED = 332;
  public const int T_STATIC = 333;
  public const int T_RETURN = 334;
  public const int T_BREAK = 335;
  public const int T_EXPORT = 336;
  public const int T_IMPORT = 337;
  public const int T_SWITCH = 338;
  public const int T_IN = 339;
  public const int T_INCONTEXTOF = 340;
  public const int T_FOR = 341;
  public const int T_WHILE = 342;
  public const int T_DO = 343;
  public const int T_IF = 344;
  public const int T_VAR = 345;
  public const int T_CONST = 346;
  public const int T_ENUM = 347;
  public const int T_GOTO = 348;
  public const int T_THROW = 349;
  public const int T_TRY = 350;
  public const int T_SETTER = 351;
  public const int T_GETTER = 352;
  public const int T_ELSE = 353;
  public const int T_CATCH = 354;
  public const int T_OMIT = 355;
  public const int T_SYNCHRONIZED = 356;
  public const int T_WITH = 357;
  public const int T_INT = 358;
  public const int T_REAL = 359;
  public const int T_STRING = 360;
  public const int T_OCTET = 361;
  public const int T_FALSE = 362;
  public const int T_NULL = 363;
  public const int T_TRUE = 364;
  public const int T_VOID = 365;
  public const int T_NAN = 366;
  public const int T_INFINITY = 367;
  public const int T_UPLUS = 368;
  public const int T_UMINUS = 369;
  public const int T_EVAL = 370;
  public const int T_POSTDECREMENT = 371;
  public const int T_POSTINCREMENT = 372;
  public const int T_IGNOREPROP = 373;
  public const int T_PROPACCESS = 374;
  public const int T_ARG = 375;
  public const int T_EXPANDARG = 376;
  public const int T_INLINEARRAY = 377;
  public const int T_ARRAYARG = 378;
  public const int T_INLINEDIC = 379;
  public const int T_DICELM = 380;
  public const int T_WITHDOT = 381;
  public const int T_THIS_PROXY = 382;
  public const int T_WITHDOT_PROXY = 383;
  public const int T_CONSTVAL = 384;
  public const int T_SYMBOL = 385;
  public const int T_REGEXP = 386;
  public const int yyErrorCode = 256;
 }
 namespace yyParser {
  using System;
  /** thrown for irrecoverable syntax errors and stack overflow.
    */
  internal class yyException : System.Exception {
    public yyException (string message) : base (message) {
    }
  }

  /** must be implemented by a scanner object to supply input to the parser.
    */
  internal interface yyInput {
    /** move on to next token.
        @return false if positioned beyond tokens.
        @throws IOException on input error.
      */
    bool advance (); // throws java.io.IOException;
    /** classifies current token.
        Should not be called if advance() returned false.
        @return current %token or single character.
      */
    int token ();
    /** associated with current token.
        Should not be called if advance() returned false.
        @return value for token().
      */
    Object value ();
  }
 }
} // close outermost namespace, that MUST HAVE BEEN opened in the prolog
