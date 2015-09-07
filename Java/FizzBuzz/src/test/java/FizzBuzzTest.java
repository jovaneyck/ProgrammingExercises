import org.junit.Test;
import org.junit.runner.RunWith;
import org.junit.runners.Parameterized;

import java.util.Arrays;
import java.util.Collection;

import static org.junit.Assert.*;

@RunWith(Parameterized.class)
public class FizzBuzzTest {
    private final int number;
    private final String expected;

    @Parameterized.Parameters
    public static Collection<Object[]> data(){
        return Arrays.asList(new Object[][]{
                {1, "1"},
                {2, "2"},
                {3, "Fizz"},
                {5, "Buzz"},
                {6, "Fizz"},
                {10, "Buzz"},
                {15, "FizzBuzz"},
        });
    }

    public FizzBuzzTest(int number, String expected){
        this.number = number;
        this.expected = expected;
    }

    @Test
    public void translatesANumberCorrectly(){
        assertEquals(this.expected, FizzBuzz.Of(this.number));
    }
}