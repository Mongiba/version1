using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;



public class mouvement : page1
{  ///speech
    public ConfidenceLevel Confidence = ConfidenceLevel.Low;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> keywords = new Dictionary<string, Action>();
    //timer et verfification
    int k = 0;
    float currentTime = 0;
    [SerializeField] 
    Text countdownText;
    int n;
    //temps reel
    public double time;
    public string tim;
    int x = 1;

    //tableau
    public int i = 0;
    public int h = 0;
    public static int hamplus = 0;
    public static int tbarkaplus = 0;
    public static int sobhanaplus = 0;
    public int c = 1;
    public static int numham;
    public int numtbarklh;
    public int numsobhanlh;
    public bool Play = false;
    public int[,] tab_wa9et;


    protected static int hamdoulh;
    protected static int tbarkalh;
    protected static int sobhanalh;
    //premeire scene

    //2em scene

    //fct recognition
    public void Start()
    {
        tritab();
        for (i = 0; i < hamdoulh + tbarkalh + sobhanalh + 2; i++)
        {
            Debug.Log(tab_wa9et[0, i]);
            Debug.Log(tab_wa9et[1, i]);
        }
        string w = DateTime.Now.ToString("H:mm:ss");
         double z = TimeSpan.Parse(w).TotalSeconds;
        
        currentTime = (tab_wa9et[0, 1] - (int)z);
         keywords.Add("hamdoulah", hamdoulah);
         keywords.Add("tbarklah", tbarklh);
         keywords.Add("sobhanalah", sobhanalah);

         keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray(), Confidence);
         keywordRecognizer.OnPhraseRecognized += Recognizeed;
    }
    //pour tableau
    public void tritab()
    {
        int duree = f - d;

        hamdoulh = ham + hamplus;
        int[,] tab_hamdoulah = new int[2, hamdoulh];

        numham = d;



        for (i = 0; i < hamdoulh; i++)
        {
            tab_hamdoulah[0, i] = numham;

            tab_hamdoulah[1, i] = 1;

            numham += (duree / (hamdoulh + 1));
        }

        tbarkalh = tbar + tbarkaplus;


        numtbarklh = d;

        int[,] tab_tbarklh = new int[2, tbarkalh];


        for (i = 0; i < tbarkalh; i++)
        {
            tab_tbarklh[0, i] = numtbarklh;
            tab_tbarklh[1, i] = 2;


            numtbarklh += (duree / (tbarkalh + 1));

        }
        numsobhanlh = d;
        sobhanalh = sobh + sobhanaplus;
        int[,] tab_sobhanlh = new int[2, sobhanalh];

        for (i = 0; i < sobhanalh; i++)
        {
            tab_sobhanlh[0, i] = numsobhanlh;

            tab_sobhanlh[1, i] = 3;

            numsobhanlh += (duree / (sobhanalh + 1));


        }





        tab_wa9et = new int[2, hamdoulh + tbarkalh + sobhanalh + 2];


        tab_wa9et[0, 0] = d;
        tab_wa9et[1, 0] = 0;



        for (i = 1; i <= hamdoulh + tbarkalh + sobhanalh + 1; i++)
        {
            if (i <= hamdoulh)
            {
                tab_wa9et[0, i] = tab_hamdoulah[0, i - 1];
                tab_wa9et[1, i] = tab_hamdoulah[1, i - 1];
            }
            else if ((i <= tbarkalh + hamdoulh) && (i > hamdoulh))
            {
                tab_wa9et[0, i] = tab_tbarklh[0, i - (1 + hamdoulh)];
                tab_wa9et[1, i] = tab_tbarklh[1, i - (1 + hamdoulh)];
            }
            else if ((i <= tbarkalh + hamdoulh + sobhanalh) && (i > hamdoulh + tbarkalh))
            {
                tab_wa9et[0, i] = tab_sobhanlh[0, i - (1 + hamdoulh + tbarkalh)];
                tab_wa9et[1, i] = tab_sobhanlh[1, i - (1 + hamdoulh + tbarkalh)];
            }
            else
            {
                tab_wa9et[0, hamdoulh + tbarkalh + sobhanalh + 1] = f;
                tab_wa9et[1, hamdoulh + tbarkalh + sobhanalh + 1] = 0;
            }


        }

        sortBySelection(tab_wa9et, hamdoulh + tbarkalh + sobhanalh + 2);

        i = 0;
        int j = 1;

        while (i < tbarkalh + hamdoulh + sobhanalh + 1)

        {
            if (tab_wa9et[0, i] == tab_wa9et[0, i + j])
            {
                while (tab_wa9et[0, i] == tab_wa9et[0, i + j])
                {
                    j++;
                }
                int diff = (tab_wa9et[0, i + j] - tab_wa9et[0, i]) / j;
                for (int k = 0; k < j; k++)
                {
                    tab_wa9et[0, i + k] = tab_wa9et[0, i + k] + k * diff;
                }
            }
            i++;
            j = 1;
        }
    }


    void sortBySelection(int[,] t, int sizeOfTheCollection)
    {

        int i, min, j;
        int x, y;
        for (i = 0; i < sizeOfTheCollection - 1; i++)
        {
            min = i;
            for (j = i + 1; j < sizeOfTheCollection; j++)
                if (t[0, j] < t[0, min])
                    min = j;
            if (min != i)
            {
                x = t[0, i];
                t[0, i] = t[0, min];
                t[0, min] = x;
                y = t[1, i];
                t[1, i] = t[1, min];
                t[1, min] = y;
            }
        }
    }



    //commencer le jeu
    /* public void play_game()

     {
         Debug.Log(time);

         if ((time < d) || (time > f))
         {
             Play = false;
             keywordRecognizer.Stop();
             c = 1;
         }
         else if ((time == tab_wa9et[0, c]) && (c < hamdoulh + tbarkalh + sobhanalh + 2))
         {
             Play = true;
             int t = tab_wa9et[0, c];
             Debug.Log(t);
             Debug.Log(c);
             //Active(t, tab_wa9et[0, c + 1], tab_wa9et[1, c]);
             c++;
         }


     }*/

    public void Active(int t, int sec, int nn)
    {



        int startingtime = ((sec - t) / 2);


        if ((k == 0) && (Play == true))
        {
            currentTime = startingtime;
            Debug.Log(startingtime);
            n = nn;
            if (n == 1)
            {

                Debug.Log("9oulhmd");
            }
            else if (n == 2)
            {
                Debug.Log("9oulh tbrklh");
            }
            else if (n == 3)
                Debug.Log("9oul sbhnlh");
           
            keywordRecognizer.Start();
            Invoke("verif", startingtime);
        } 
    }
    void verif() 
    { 
                Debug.Log("wfewa9t");
                if (k == 1)
                {
                    Play = false;
                    keywordRecognizer.Stop();
                    k = 0;
                }
                else if (k == 0)
                {
                    Play = false;
                    keywordRecognizer.Stop();
                    if (n == 1) { hamplus++; }
                    else if (n == 2) { tbarkaplus++; }
                    else if (n == 3) { sobhanaplus++; }
                    else if (n == 0) { Debug.Log("debut ou fin"); }

                }
                
              
                c++;
                currentTime = 0;
                x = 1;
    }
        
    


    private void Recognizeed(PhraseRecognizedEventArgs speech)
    {


        if ((n == 1) && keywords[speech.text] == hamdoulah)
        {Debug.Log("9oul hamdoulah");
            keywords["hamdoulah"].Invoke();
        }
        else if ((n == 3) && keywords[speech.text] == sobhanalah)
        {
            keywords["sobhanalah"].Invoke();
        }
        else if ((n == 2) && keywords[speech.text] == tbarklh)
        { 
            keywords["tbarklh"].Invoke();
        }
    }
    private void hamdoulah()
    {
        Debug.Log("hamdoulah");
        k = 1;
    }
    private void tbarklh()
    {

        Debug.Log("tbarklh");
        k = 1;
    }
    private void sobhanalah()
    {
        Debug.Log("sobhanalah");
        k = 1;
    }





    private void Update()
    {

        time = TimeToSeconds();

       double TimeToSeconds()
        {
            tim = DateTime.Now.ToString("H:mm:ss");
            double seconds = TimeSpan.Parse(tim).TotalSeconds;
            return seconds;

        }

        play_game();

        void play_game()

        {



            if ((time < d) || (time > f))
            {
                Play = false;
                //keywordRecognizer.Stop();
                c = 1;
            }
            if (time == tab_wa9et[0, c]&&(x==1))
            {

                Play = true;
                x = 0;
                Active(tab_wa9et[0, c], tab_wa9et[0, c + 1], tab_wa9et[1, c]);


            }


        }
        
        
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0");
        
        if (currentTime <= 0)
        {
            currentTime = 0;
            
        }
    }
}