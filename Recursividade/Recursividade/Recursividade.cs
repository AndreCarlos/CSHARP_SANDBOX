namespace Recursividade
{
   public class Recursividade
    {
        int resultado = 0;

        public int Recursive(int num)
        {
            if (num < 0)
                resultado = -1;

            if ((num == 0) || (num == 1))
                resultado = 1;
            else
                resultado = num * Recursive(num - 1);

            return resultado;
        }

         
    }
}
