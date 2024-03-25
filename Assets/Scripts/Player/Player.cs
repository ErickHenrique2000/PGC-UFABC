using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public  Animator        anima;
    public  Transform       player;
    public  GameObject      poder;
    public  bool            Jump;
    public  bool            Slide;
    public  bool            podePular;
    public  bool            animaPulo;
    public  bool            animaSlide;
    public  bool            animaAndar;
    public  bool            podeTomarDano;
    public  Rigidbody2D     RigidBody;
    public  bool            grounded;
    public  bool            levouTiro;
    public  bool            recebeVida;
    public  bool            TiroEmRecarga;
    public  bool            teste;
    public  bool            ladoE;
    public  bool            tpEmRecarga;
    public  bool            animaTP;
    public  bool            jadeudash;
    public  LayerMask       ground;
    public  LayerMask       layerTiro;
    public  LayerMask       coracao;
    public  Transform       groundCheck;
    public  Transform       heartCheck;
    public  float           velocidade;
    public  int             vida;
    public  float           toop;
    public  float           tpforce;
    public  float           jumpForce;
    public  float           tempoDano;
    public  float           slideForce;
    public  float           pulos;
    public  float           tempoPosDano;
    public  float           tempoRecarregandoDash;
    public  float           recargaDash;
    public  float           TimeAnimationDash;
    public  float           TempoDeRecargaTiro;
    public  float           tempoemtp;
    public  float           tpdandotp;
    public  float           tempoposdash;
    public  float           recargaTp;
    private float           resultanteX;
    private float           resultantedash;
    private float           temponodash;
    public  float           tempoEmRecargaTiro;
    public  static  float   adicionalVelocidade;
    public  static  bool    adicionavida;
    public  static  float   Xpersonagem;
    public  static  float   Ypersonagem;
    public static  float    horinzontal;
    public  static  float   TempoRecargaSlide;
    // Start is called before the first frame update
    void Start()
    {
        horinzontal = 1;
        tempoPosDano = 0;
        tempoEmRecargaTiro = 0;
        TempoRecargaSlide = 3;
    }

    // Update is called once per frame
    void Update()
    {
        Xpersonagem = player.position.x;
        Ypersonagem = player.position.y;
        tempoPosDano+=Time.deltaTime;
        if(TiroEmRecarga == true){
            tempoEmRecargaTiro += Time.deltaTime;
            if(tempoEmRecargaTiro >= TempoDeRecargaTiro){
                TiroEmRecarga = false;
            }
        }
        //recebeVida = Physics2D.OverlapCircle(heartCheck.position, 0.2f, coracao);
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
        levouTiro = Physics2D.OverlapCircle(groundCheck.position, 0.2f, layerTiro);
        if(grounded){
            podePular = true;
            pulos = 0;
        }
        if(Input.GetAxisRaw("andar") != 0 && animaTP == false){
            horinzontal = Input.GetAxisRaw("andar");
            animaAndar=true;
            resultanteX = transform.position.x + (horinzontal*(velocidade+adicionalVelocidade)*Time.deltaTime);
            transform.position = new Vector3(resultanteX, transform.position.y, transform.position.z);
        }else{
            animaAndar = false;
        }
        if(Input.GetButtonDown("Jump") && podePular == true && pulos==0){
            RigidBody.AddForce(new Vector2(0, jumpForce));
            Jump = true;
            pulos++;
        }
        if(Input.GetButtonDown("Slide") && grounded == true && Slide == false){
            resultantedash = slideForce*horinzontal;
            RigidBody.AddForce(new Vector2(resultantedash, 0));
            Slide = true;
            temponodash = 0;
            tempoRecarregandoDash = 0;
        }
        if(Input.GetButtonDown("Fire3") && TiroEmRecarga == false){
            GameObject tempPrefab = Instantiate(poder) as GameObject;
            TiroEmRecarga = true;
            tempoEmRecargaTiro = 0;
            tempPrefab.transform.position = new Vector3(transform.position.x,transform.position.y - 0.25f , tempPrefab.transform.position.z);
        }
        if(Input.GetButtonDown("tp") && tpEmRecarga == false && Slide == false && Jump == false){
            animaTP = true;
            tempoemtp = 0;  
            tempoposdash = 0;
            jadeudash = false;
            tpEmRecarga = true;
            
        }
        if(tempoposdash >= recargaTp){
            tpEmRecarga = false;
        }else{
            tempoposdash += Time.deltaTime;
        }
        if(tempoemtp > tpdandotp){
            animaTP = false;
        }else{
            tempoemtp += Time.deltaTime;
        }
        if(Slide == true){
            temponodash+=Time.deltaTime;
            tempoRecarregandoDash+=Time.deltaTime;
            TempoRecargaSlide = tempoRecarregandoDash;
        }
        if(tempoRecarregandoDash>=recargaDash){
            Slide = false;
        }
        if(grounded == true){
            Jump = false;
        }else{
            Jump = true;
        }
        if(Slide == true && grounded == true && temponodash<TimeAnimationDash){
            animaSlide = true;
        }else{
            animaSlide = false;
        }
        if(levouTiro == true && podeTomarDano == true){
            vida--;
            tempoPosDano = 0;
            podeTomarDano = false;
        }
        if(tempoPosDano >= tempoDano){
            podeTomarDano = true;
        }
        if(vida == 0){
            SceneManager.LoadScene("menu");
        }
        /*if(recebeVida == true){
            vida++;
        }*/
        if(adicionavida){
            adicionavida = false;
            vida++;
        }
        if(horinzontal < 0 && teste == true){
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            teste = false;
            ladoE = true;
        }
        if(horinzontal > 0){
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
            if(ladoE){
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                ladoE = false;
            }
            teste = true;
        }

    if(animaTP == true){
        if(horinzontal >= 0 && tempoemtp >= 0.5f && jadeudash == false){
            jadeudash = true;
            transform.position = new Vector3(transform.position.x + tpforce, transform.position.y, transform.position.z);
        }else if(horinzontal < 0 && tempoemtp >= 0.5f && jadeudash == false){
            jadeudash = true;
            transform.position = new Vector3(transform.position.x - tpforce, transform.position.y, transform.position.z);
        }
        
    }

        anima.SetBool("pular", Jump);
        anima.SetBool("slide", animaSlide);
        anima.SetBool("andar", animaAndar);
        anima.SetBool("tp", animaTP);
    }

    void FixedUpdate(){

    }

    void OnTriggerEnter2D(){
        
    }
}
