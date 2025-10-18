public class Nodo
{
    public string lexema;
    public int token;
    public int renglon;
    public Nodo? sig;

    public Nodo(string lexema, int token, int renglon)
    {
        this.lexema = lexema;
        this.token = token;
        this.renglon = renglon;
        this.sig = null;
    }
}