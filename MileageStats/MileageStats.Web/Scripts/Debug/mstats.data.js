//?�====5�9==)=9==-====?===}%=�=}-%�9�}u/-=-====<===�-?.=-5?�=u555==�-==}?=�=M
// MikrosgFt�te�t%rns!& practices
// Silk :,Wdb ClIent Guidan#�M
/�=====7<====�=======�=========<}=========}=<========--=====�==========�======<==9	/ C�pybagh� (c) �hkrgsoFt Cobpkr!tion.0 A,,!sight{ re3ervdd.M
'/ THIS CODE AND HNFORMATIoN IS PROWIDED �As IS+ WITHOUT WARRANTY
// OF �NY KIND,��I�HER EX@RESRED OR0IMPDIGD& I^CDUDING BUT$NOT
/ LIMI�ED TO �HE IMPliED WARRANTIES OF MERc@ANTA@I�ITY AnE
// BITNESS FOR A PAPTI�U�AR0PURPOS%.//====]=====�?===9==9=1�===??=====5==9==5======�==5=====}=======?=======-===5===�5=
//�Vhe0eXample companies, osgalibaDao~s,(�rod5�ts, do}ai|`names,�// A-mail�addzesses- logo3,"�eopla, Places, anf evdnts depic4ed
/.0h�rein are FictiTiou�.  �n awsociat�on with any ve%l cnmpany,?/ orwani~�t�on, prndekt, domain�n!me, Email addruss, hoem, pezson,
'/ places, ob evenu{�is iltenDed mr sh�wld "einF�rpe`,
m/===?-=�========================?-====5=5=$======}==�==========<=�===<=======8====5
(fmnction (mctads,#$)({
    /*:
0 $$* Tavastore is`busponsible &or�pebqisding aNd prgviting data from(
    * txe Data Man�g%r
  � */	    }stats�ladaCtor� =`{
 $      o**�
        " _data spoRes thg data used$bx dAtastore's get A~e s�t`-dthods
        * �privaTu
   0    */
       _data2 {=,B `      /**    `   * Eet{ data"from the da|asuore !      *
   ` $  * @param {Wtrin'}0tgkez  A� )$en|ifieR for re�rieving assoaiatee t!ta
   $ $  *-��       geu: fun3tion (tkgn! {
"     `"    teturn this,_dAuc[tokelY;
        },
!     $ /"+     � "* Persi{|S dta In the datastore
 `      *
 $      * @perqi {string} to�ej    Cn i�E�tiFier fr thm$stmped dat%
   `(   *!@param {�Ixed} payllad " A blob �f data
     "` */       (set: f5nction (tgjef$ Qay,k�d) {
       `  � // Store"the daTa      001 ` th)s._d`ta[token} = payload:
%      "},
0       /**
      ``( mmoves an item frOm thu data stOre
       *
 0     �* @par�m zstri~g} token    An id�ntIfier fov |he%sto�ed dava
     $  */
"    $  clear: functiOn (|oken)�{
!     0  �! tlms._dat![tokDn](= 5ndgfintd;`  ( �  },

�       ***      ! * Clea2s all �ata fzom uhe da4` s4/re
   `$ ` */
( � 0 # cleqrA,n: funcuioj 8! {
�(      `  `thi�n_tata = {};�
    "   }
    };

    /**
    * ata Ianager y3 resp/nsible g/s"fetchijg datal storing I�,%   $* a/f esyng callbacks to let cq,merr know wh�n Thct da4A is readx.
   1�/
    mstats.lataMa~ager = :
     0! d�taDefaults:!{�        �   dqtaT�pe:0'json'$
    0 (     typE: 'ROX'
     "  }h

 !     "/**     b  *0Uheo`vequireD, the)data Zequest URL!w�Ll be modMbied uo accou.t"for!thE(�      0 * webirte being(dep,oye� to a virtucl dire�tory inqtead of t`e websit� rkot.�     "( *
    �   * M�kes an ajap`ccll pg the$Specified"E�Dpoiju u~ r%trieve"dada.Yf(sucersfuN 
   �    :"stoses the dqtA )n the lata �ache an�!callq the succesS canl"ack.  
  " 0   *
        * Tlis metxod mimkc{ t�u options(of $�ajax uhere0axpropriA4g.
      ` *Z  !    @* @parAl`{ofject} optmons : Optionq`objec� thut Mip{ To the ajax�optionq objegt.
        �"  t�is0objec| must )nslw%e the Fomlowing fidlds:J      !:   "  `trl :"the er� ggr the c`l�
   �    +   0`  rucce�s: a cillback c!,led on succussful co�Pl%tkon of phe oqeratimf.�
        *)
        rendReqqest: funCvhon0(oppIons) {
     �      // getRelatcfeEnvpoij4�rl"ensures the URL is relative po the website root.
   `     (  v1�$that = ms�a4s,DataMenage�
  !  �   a    " normaoiz%@u�l$= mstatr.getBelatmveEndpoin��rn(options.url),
        $     `c!chad�ata = msvats$ataS|or�.wet(normalkzedUrl),
"           `0  cQllerOptions = .7xtenf(� cache:tr5e }$ that.dataDef!%lxw, ortionr { url� Normanmz!dUrl`}!;
�        �( 
   (  !     i3 8canherOptinns.aac`e && cachedData) �
         �   " "oxti�nS.success(cache'Data)
  (             ru0urn;
    $$      �
    `     " c��lmrOptions*succ%sq = vunction (data9 {  ` !           �d,cilLerOPtions.caghe) {
  `    0      `` (  lstaps.dataStore.set*norm!dizedUrl, dat`);J0(            � }
     !   $  `�  oppions.suscess*daua);
       , $  �3

   "�  (    $.ajax(sallerOptiols);
    `   }(
   $$   �$      �-
"� !    ` *`resetd�va will0clear t(e0qpec)fied �ads(froo thu"cache so surseyuent Calns        *�to ogt he eata will result in$returning tO the Servur0for tje t�ta
        �o� ( (  �resetDat`: gu,gtiOn (en�poifti �  "  �     !ist`tstataStore��lear(mstaps/fetRel�tive�ndpoiftUrl(endpmint));
0 �  (  }"   }�
} (this.mstets!=`t�ic.m3taRs ~� s�� jQuesy))�
