import org.junit.runner.RunWith
import org.scalatest.junit.JUnitRunner
import org.scalatest.matchers.ShouldMatchers
import org.scalatest.FlatSpec

@RunWith(classOf[JUnitRunner])
class CalculatorSpec extends FlatSpec with ShouldMatchers{
	behavior of "add"
	
	val calculator = new StringCalculator
	
	it should "return 0 for an empty input" in {
	  calculator.add("") should be(0)
	}
}