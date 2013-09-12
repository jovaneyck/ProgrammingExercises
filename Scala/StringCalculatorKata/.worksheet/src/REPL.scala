object REPL {;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(57); 
  println("Welcome to the Scala worksheet");$skip(40); 
  
  val list = "1,2" split(",") toList;System.out.println("""list  : List[String] = """ + $show(list ))}
}
