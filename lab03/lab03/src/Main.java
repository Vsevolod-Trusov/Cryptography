import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        while (true) {
            System.out.println("\nSelect operation:");
            System.out.println("1 - NOD for two numbers");
            System.out.println("2 - NOD for three numbers");
            System.out.println("3 - Search primary numbers in the interval");
            System.out.println("Select value: ");
            int inputValue = scanner.nextInt();

            System.out.println();

            switch (inputValue) {
                case 1 : {
                    System.out.println("Insert 2 numbers: ");
                    int parmA = scanner.nextInt();
                    int parmB = scanner.nextInt();
                    int result = getNodTwoNumbers(parmA, parmB);
                    System.out.println("NOD equals: " + result);
                    break;
                }
                case 2 : {
                    System.out.println("Insert 2 numbers: ");
                    int parmA = scanner.nextInt();
                    int parmB = scanner.nextInt();
                    int parmC = scanner.nextInt();
                    int result = getNodThreeNumbers(parmA, parmB, parmC);
                    System.out.println("NOD equals: " + result);
                    break;
                }
                case 3 : {
                    System.out.println("Insert start-number and finish-number: ");
                    int startValue = scanner.nextInt();
                    int finishValue = scanner.nextInt();

                    for (int i = startValue; i <= finishValue; i++) {
                        if (isPrimary(i))
                            System.out.print(i + " ");
                    }
                    System.out.println();
                    break;
                }


                default :
                    System.out.println("Wrong value");
                    break;

            }
        }
    }

    public static int getNodTwoNumbers(int parmA, int parmB) {
        while (parmA != 0 && parmB != 0) {
            if (parmA > parmB) {
                parmA = parmA % parmB;
            }
            else {
                parmB = parmB % parmA;
            }
        }
        return parmA + parmB;
    }

    public static int getNodThreeNumbers(int parmA, int parmB, int parmC) {
        return getNodTwoNumbers(getNodTwoNumbers(parmA, parmB), parmC);
    }

    public static String getPrimaryNumbersMultiplyString (int inputNumber) {
        StringBuilder resultMultiply = new StringBuilder();
        int divider = 2;

        while (inputNumber > 1) {
            if (inputNumber % divider == 0) {
                resultMultiply.append(divider).append(" * ");
                inputNumber /= divider;
                divider = 2;
                continue;
            }
            divider++;
        }
        return resultMultiply.substring(0, resultMultiply.length()-1);
    }

    public static boolean isPrimary (int inputNumber) {
        if (inputNumber > 1){
            for (int i = 2; i <= (inputNumber/2); i++) {
                if (inputNumber % i == 0)
                    return false;
            }
        }
        else
            return false;

        return true;
    }

    public static void getEuclid (int parmA, int parmB)
    {
        parmA = parmA % parmB;
        for (int i = 1; i < parmB; i++)
        {
            if ((parmA * i) % parmB == 1)
            {
                System.out.println("mod equals: " + i);
                break;
            }
        }
    }
}