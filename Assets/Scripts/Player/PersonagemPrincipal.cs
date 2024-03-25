using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersonagemPrincipal : MonoBehaviour
{
    //formas
    public  bool        formaHumana;
    public  bool        formaAnimal;
    // teste
    // qualquer coisa

    //movimento
    public  bool        andando;
    public  bool        ladoE;
    public  bool        ladoD;
    public  float       velocidade;
    public  float       lado;
    public  static  float   ladostatic;
    public  float       resultante;
    public  Animator    anima;
    public  static  float   posYPlayer;
    public  static  float   posXPlayer;
    public  bool        podeSeMover;
    private float       tempoSemMover;
    //pulo
    public  Rigidbody2D personagem;
    public  Transform   groundCheck;
    public  LayerMask   chao;
    public  bool        Jump;
    public  bool        grounded;
    public  float       JumpForce;
    private float       tempoPulando;
    public  float       tempoDePulo;
    public  int         numeroPulos;
    private bool        caindo;
    private bool        DuploPulo;
    private float       tempoForaDoChao;
    //tp
    public  bool        possuitp;
    private bool        podeDarTp;
    private bool        deuTp;
    public  float       recargaTp;
    private float       tempopostp;
    public  float       tpRange;
    public  Transform   paredeCheck;
    private bool        paredeNoTp;
    private bool        darTP;
    public  GameObject  painelTP;
    public  Animator    animadorTP;
    //vida
    public  float       ladoInimigo;
    public  bool        sofreuDano;
    public  float       vidaMax;
    public  float       vida;
    public  float       vidaAtual;
    public  float       taxaRegenVida;
    private float       tempoRegen;
    public  float       qtdRegenVida;
    public  LayerMask   Enemy;
    public  float       velocidadePosDano;
    public  Transform   EnemyCheck;
    public  bool        invencivel;
    public  float       tempoInvencivel;
    private bool        knockBack;
    private float       tempoNoKnockBack;
    //fogo grego
    public  GameObject  fogo;
    public  bool        possuiFogo;
    private bool        TiroEmRecarga;
    private float       tempoEmRecargaTiro;
    public  float       tempoDeRecargaTiro;
    public  GameObject  painelFogo;
    //escudo
    private bool        usandoEscudo;
    public  Animator    animadorFogo;
    public  float       tempoDeRecargaEscudo;
    private float       tempoEmRecargaEscudo;
    public  float       tempoMaxUsandoEscudo;
    private float       tempoUsandoEscudo;
    //metamorfose
    public  bool        possuiAguia;
    private bool        podeSeTransformar;
    private bool        recargaMetamorfose;
    private bool        voandoHorizontal;
    private bool        voandoVertical;
    public  float       velocidadeVoo;
    private bool        ladoDAve;
    private bool        ladoEAve;
    public  float       velocidadeVooVertical;
    private float       resultanteVooVertical;
    public  float       ladoVertical;
    private float       tempoParaVoltar;
    public  float       tempoMinVoando;
    public  float       tempoDeRecargaMeta;
    private float       tempoMetaEmRecarga;
    public  float       Subir;
    private float       timer;
    public  GameObject  painelPru;
    public  Animator    PruPru;
    //lança
    private bool        lanca;
    private float       tempoLanca;
    public  Animator    animadorLanca;
    //espada
    private bool        espada;
    private float       tempoEspada;
    public  Animator    animadorEspada;
    //dash metamorfose
    private bool        podedardash;
    private float       tempoNoDash;
    public  float       forcaDashAve;
    private float       resultanteDashAve;
    private bool        canseladash;
    public  Transform   dashCheck;
    public  bool        paredeEmFrente;      
    
    // Start is called before the first frame update
    void Start()
    {
        loadGameStats();
        formaAnimal = false;
        formaHumana = true;
        ladoD = true;
        ladoE = false;
        vidaAtual = vidaMax;
        tempoRegen = 0;
        timer = 1;
        podeSeMover = true;
        tempoSemMover = 0;
        podedardash = true;
    }

    // Update is called once per frame
    void Update()
    {
        painelPru.SetActive(possuiAguia);
        painelFogo.SetActive(possuiFogo);
        painelTP.SetActive(possuitp);
        if(transform.position.y <= -70){
            vidaAtual = 0;
        }
        ladostatic = lado;
        vida = vidaAtual;
        paredeEmFrente = Physics2D.OverlapCircle(dashCheck.position, 0.6f, chao);
        paredeNoTp = Physics2D.OverlapCircle(paredeCheck.position, 0.1f, chao);
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, chao);
        posYPlayer = transform.position.y;
        posXPlayer = transform.position.x;
        if(timer <= 1){
            timer+=Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y + (Subir*Time.deltaTime), transform.position.z);
        }
        if(sofreuDano == true && invencivel == false){
            vidaAtual -= 10;
            podeSeMover = false;
            tempoSemMover = 0;
            invencivel = true;
            knockBack = true;
            tempoNoKnockBack = 0;
            if(andando){
                andando = false;
            }
        }
        if(sofreuDano == true){
            if(Jump){
                Jump=false;
            }
        }
        if(knockBack == true){
            if(ladoInimigo>=0){
            transform.position = new Vector3(transform.position.x + (velocidadePosDano*Time.deltaTime), transform.position.y, transform.position.z);
            }else{
            transform.position = new Vector3(transform.position.x - (velocidadePosDano*Time.deltaTime), transform.position.y, transform.position.z);
            }
            if(tempoNoKnockBack >=0.5f){
                knockBack = false;
                sofreuDano = false;
            }else{
                tempoNoKnockBack += Time.deltaTime;
            }
        }
        if(invencivel == true){
            if(tempoInvencivel>=2.5f){
                invencivel = false;
                tempoInvencivel = 0;
            }else{
                tempoInvencivel+= Time.deltaTime;
            }
        }
        if(podeSeMover == false){
            if(tempoSemMover>=1f){
                podeSeMover = true;
            }else{
                tempoSemMover+=Time.deltaTime;
            }
        }

        if(lanca){
            if(tempoLanca>=0.5f){
                lanca = false;
                
            }else{
                tempoLanca +=Time.deltaTime;
            }
        }
        if(espada){
            if(tempoEspada>=0.25f){
                espada = false;
                if(tempoEspada<0.5f){
                    tempoEspada += Time.deltaTime;
                }
            }else{
                tempoEspada += Time.deltaTime;
            }
        }

        if(vida<=0){
            SceneManager.LoadScene("GameOver");
        }
        if(formaHumana == true && podeSeMover && espada == false){
            if(Input.GetAxisRaw("Horizontal") != 0 && deuTp == false && usandoEscudo == false && podeSeMover == true && espada == false){
                lado = Input.GetAxisRaw("Horizontal");
                andando = true;
                resultante = lado * velocidade * Time.deltaTime;
                transform.position = new Vector3(transform.position.x + resultante, transform.position.y, transform.position.z);
                if(ladoE && lado > 0){
                    transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                    ladoE = false;
                    ladoD = true;
                }
                if(ladoD && lado < 0){
                    transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                    ladoE = true;
                    ladoD = false;
                }
            }else{
                andando = false;
            }

            if(grounded){
                numeroPulos = 0;
                tempoForaDoChao = 0;
            }else {
                tempoForaDoChao += Time.deltaTime;
            }
            if(tempoForaDoChao >= 3f){
                Jump = false;
                DuploPulo = false;
            }
            
            if(Input.GetButtonDown("Jump") && numeroPulos<1 && usandoEscudo == false){
                Jump = true;
                numeroPulos++;
                tempoPulando = 0;
                personagem.velocity = Vector2.up * JumpForce;
                if(numeroPulos >0){
                    DuploPulo = true;
                }
            }else{
                 if(Jump){
                    if(grounded && tempoPulando>= 0.1){
                        Jump = false;
                    } else if(tempoPulando<0.1){
                        tempoPulando+=Time.deltaTime;
                    }
                }
            }
            if(grounded == true && DuploPulo == true){
                DuploPulo = false;
            }

            if(Input.GetButtonDown("tp") && podeDarTp && usandoEscudo == false && paredeNoTp == false && grounded == true){
                deuTp = true;
                tempopostp = 0;
                podeDarTp = false;
                invencivel = true;
                tempoInvencivel = 1.5f;
                darTP = true;
            }
            if(darTP && tempopostp >= 0.5f){
                transform.position = new Vector3(transform.position.x + (lado*tpRange), transform.position.y, transform.position.z); 
                darTP = false;
            }
            if(deuTp && tempopostp >= 1f){
                deuTp = false;
            }

            if(vidaAtual < vidaMax){
                tempoRegen += Time.deltaTime;
                if(tempoRegen >= taxaRegenVida){
                    vidaAtual += qtdRegenVida;
                    tempoRegen = 0;
                    if(vidaAtual > vidaMax){
                        while(vidaAtual > vidaMax){
                            vidaAtual--;
                        }
                    }
                }
            }

            if(Input.GetButtonDown("lanca") && Jump == false){
                lanca = true;
                tempoLanca = 0;
                podeSeMover = false;
                tempoSemMover = 0.5f;
            }

            if(Input.GetButtonDown("espada")){
                espada = true;
                tempoEspada = 0;
            }

            if(Input.GetButtonDown("Fire3") && TiroEmRecarga == false && usandoEscudo == false && possuiFogo){
                GameObject tempPrefab = Instantiate(fogo) as GameObject;
                TiroEmRecarga = true;
                tempoEmRecargaTiro = 0;
                tempPrefab.transform.position = new Vector3(transform.position.x,transform.position.y , tempPrefab.transform.position.z);
            }   

            if(tempoMetaEmRecarga>=tempoDeRecargaMeta){
                podeSeTransformar = true;
            }else{
                podeSeTransformar = false;
            }
                      

            if(Input.GetButtonDown("meta") && grounded && tempoMetaEmRecarga>=tempoDeRecargaMeta && usandoEscudo == false && possuiAguia){
                formaAnimal = true;
                formaHumana = false;
                ladoDAve = ladoD;
                ladoEAve = ladoE;
                personagem.gravityScale = 0;
                tempoParaVoltar = 0;
                timer = 0;
            }
            if(tempoMetaEmRecarga<tempoDeRecargaMeta){
                tempoMetaEmRecarga+=Time.deltaTime;
            }

            if(Input.GetButtonDown("escudo") && grounded){
                if(usandoEscudo == true){
                    usandoEscudo = false;
                    tempoEmRecargaEscudo = 0;
                }else if(usandoEscudo == false && tempoEmRecargaEscudo >= tempoDeRecargaEscudo){
                    usandoEscudo = true;
                    tempoUsandoEscudo = 0;
                }
            }
            if(tempoEmRecargaEscudo < tempoDeRecargaEscudo){
                tempoEmRecargaEscudo += Time.deltaTime;
            }
            if(tempoUsandoEscudo >= tempoMaxUsandoEscudo && usandoEscudo == true){
                usandoEscudo = false;
                tempoEmRecargaEscudo = 0;
            }
            if(tempoUsandoEscudo < tempoMaxUsandoEscudo && usandoEscudo == true){
                tempoUsandoEscudo+=Time.deltaTime;
            }
            if(grounded == false && Jump == false){
                caindo = true;
            }else{
                caindo = false;
            }
            
        }
        //////////////////////////////////////////////////////////
        ////////////////Fim da forma humana///////////////////////
        //////////////////////////////////////////////////////////


        if(podeDarTp == false && tempopostp >= recargaTp){
                podeDarTp = true;
            }else{
                tempopostp += Time.deltaTime;
            }

        if(TiroEmRecarga == true){
                tempoEmRecargaTiro+=Time.deltaTime;
                if(tempoEmRecargaTiro>=tempoDeRecargaTiro){
                    TiroEmRecarga = false;
                }
            }
        
        if(formaAnimal == true){
            
            
            if(Input.GetAxisRaw("Horizontal") != 0 && podeSeMover){
                lado = Input.GetAxisRaw("Horizontal");
                voandoHorizontal = true;
                resultante = lado * velocidadeVoo * Time.deltaTime;
                transform.position = new Vector3(transform.position.x + resultante, transform.position.y, transform.position.z);
                if(ladoEAve && lado > 0){
                    transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                    ladoEAve  = false;
                    ladoDAve  = true;
                }
                if(ladoDAve  && lado < 0){
                    transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                    ladoEAve  = true;
                    ladoDAve  = false;
                }
            }else{
                voandoHorizontal = false;
            }
            if(Input.GetAxisRaw("Vertical") != 0 && podeSeMover){
                voandoVertical = true;
                ladoVertical = Input.GetAxisRaw("Vertical");
                resultanteVooVertical = ladoVertical * velocidadeVooVertical * Time.deltaTime;
                transform.position = new Vector3(transform.position.x, transform.position.y + resultanteVooVertical, transform.position.z);
            }else{
                voandoVertical = false;
            }

            if(Input.GetButtonDown("meta") && tempoParaVoltar>= tempoMinVoando){
                formaAnimal = false;
                formaHumana = true;
                ladoD = ladoDAve;
                ladoE = ladoEAve;
                personagem.gravityScale = 1;
                tempoMetaEmRecarga = 0;
            }
            if(tempoParaVoltar<tempoMinVoando){
                tempoParaVoltar+= Time.deltaTime;
            }
            //dash
            if(Input.GetButtonDown("dash") && podedardash){
                tempoNoDash = 0;
                podedardash = false;
                resultanteDashAve = forcaDashAve * lado;
                personagem.AddForce(new Vector2(resultanteDashAve, 0));
                canseladash = true;
            }
            if(podedardash == false){
                if(tempoNoDash>=0.5f && canseladash){
                    personagem.velocity = new Vector2(0 , 0);
                    canseladash = false;
                }
                if(tempoNoDash >= 5){
                    podedardash = true;
                }else{
                    tempoNoDash+=Time.deltaTime;
                }
            }
        }
        //////////////////////////////////////////////////////////
        ////////////////Fim da metamorfose////////////////////////
        //////////////////////////////////////////////////////////
        animadorEspada.SetBool("espadaRecarga", espada);
        animadorLanca.SetBool("lancaRecarga", lanca);
        anima.SetBool("voando", formaAnimal);
        anima.SetBool("andar", andando);
        anima.SetBool("DuploPulo", DuploPulo);
        anima.SetBool("caindo", caindo);
        anima.SetBool("lanca", lanca);
        anima.SetBool("pulando", Jump);
        animadorFogo.SetBool("fogoRecarga", TiroEmRecarga);
        animadorTP.SetBool("Tp_recarga",!podeDarTp);
        PruPru.SetBool("PruPru_recarga", !podeSeTransformar);
        
    }

    public void loadGameStats()
    {
        //PlayerInfo info = SaveLoad.LoadPlayer();

        //if(info != null)
        //{
        //    possuiFogo = info.possuiFogo;
        //    possuiAguia = info.possuiAguia;
        //    possuitp = info.possuiTp;
        //    Debug.Log("Carreguei!");
        //}
    }

    public float GetVida()
    {
        return vida;
    }
}
