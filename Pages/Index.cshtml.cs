using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Wordle.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string Guess { get; set; }
    public string Target {get; set;}

    public List<string> Guesses { get; set; } = new();
    public List<string> Outcomes {get; set;} = new();

    public static readonly HashSet<string> Dictionary = new HashSet<string>
    {
        // A
    "ABACUS","ACCEPT","ACTION","ADVICE","AGENCY","ALMOND","ALWAYS","AMOUNT","ANCHOR","ANIMAL",
    "ANSWER","APPEAL","APPLES","ARDENT","AUTHOR","AUTUMN","AVENUE","AWARDS","AWOKEN","AZURES",

    // B
    "BALLET","BANANA","BANNER","BOTTLE","BRIDGE","BRIGHT","BROKER","BUTTON","BUBBLE","BUILDS",
    "BUNDLE","BURGER","BURSTS","BUSIER","BUTLER","BUYERS","BUZZED","BYLAWS","BYWORD","BYPASS",

    // C
    "CANDLE","CAREER","CASTLE","CENTER","CHERRY","CLIENT","COFFEE","COLORS","CREATE","CUSTOM",
    "COTTEN","CREDIT","CRISIS","CROWDS","CRUNCH","CRYSTL","CULTUR","CURSOR","CUSTOM","CYCLIC",

    // D
    "DANCER","DEMAND","DESIGN","DOCTOR","DRIVER","DREAMS","DRUMBE","DRYING","DUCKED","DUSTER",
    "DUTIES","DWELLS","DYNAMO","DYNAST","DYEING","DYKONS","DYSPNE","DYSTIC","DYSTON","DYVINE",

    // E
    "EDITOR","ENERGY","ENGAGE","ENGINE","ENRICH","ENSURE","ENTERS","ENTITY","ENVIES","EQUALS",
    "ESCAPE","ESSAYS","ESTATE","ETHICS","EVENLY","EVOLVE","EXACTS","EXCEED","EXCEPT","EXCUSE",

    // F
    "FAMOUS","FINGER","FLOWER","FOREST","FRIEND","FUTURE","FUSION","FABLES","FACETS","FACTOR",
    "FAILED","FAIRLY","FALLEN","FAMINE","FANCYS","FARCES","FARMER","FASHIO","FASTEN","FATHER",

    // G
    "GARDEN","GLOBAL","GOLDEN","GROUND","GUILTY","GUITAR","GALAXY","GATHER","GENDER","GENIUS",
    "GENTLE","GERMAN","GHOULS","GIANTS","GIVING","GLANCE","GLIDER","GLOBAL","GLORIE","GLYPHS",

    // H
    "HANDLE","HEALTH","HEIGHT","HONEST","HUNTER","HUMANE","HUMBLE","HUMOUR","HUNGER","HURRIC",
    "HUSHED","HYBRID","HYDROX","HYENAS","HYPERS","HYPHEN","HYPING","HYPOTH","HYSSOP","HYZONS",

    // I
    "INSIDE","IMPACT","IMPORT","INCOME","INDEED","INFANT","INFORM","INSIST","INVEST","IRONIC",
    "ISLAND","ISSUES","ITALIC","ITSELF","IVYING","IZARDS","IZZARD","IZZLES","IZZONS","IZZORS",

    // J
    "JACKET","JUDGES","JUMBLE","JUNGLE","JUNIOR","JUSTLY","JOLTED","JOURNY","JOVIAL","JOYFUL",
    "JOYING","JUBILE","JUGGLE","JUICED","JUICES","JUMPER","JUNKED","JUNKER","JURORS","JUSTIC",

    // K
    "KITTEN","KIDNEY","KINDLY","KINGLY","KIOSKS","KISSES","KITCHN","KNIGHT","KNIVES","KNOCKS",
    "KNOTTY","KOALAS","KOSMOS","KRAKEN","KRONOS","KUDZUS","KUMQUA","KYPHON","KYRIOS","KYTHES",

    // L
    "LADDER","LATEST","LAUNCH","LAWYER","LEADER","LEGEND","LETTER","LIGHTS","LITTLE","LIVING",
    "LOVELY","LOBBYS","LOGICS","LONGER","LOOKED","LOOPED","LOSERS","LOVING","LOWEST","LOYALY",

    // M
    "MARKER","MARKET","MASTER","MEMBER","MIDDLE","MOBILE","MODERN","MOTION","MOTHER","MOUNTS",
    "MUSICS","MUSTER","MUTUAL","MYRIAD","MYTHIC","MYXOMA","MYXONS","MYXOTE","MYXOUS","MYXURE",

    // N
    "NATIVE","NATURE","NIGHTS","NUMBER","NOBLES","NORMAL","NOTICE","NOVELS","NUANCE","NUDITY",
    "NUKING","NUMBLY","NURSES","NUTMEG","NYMPHS","NYSSAS","NYXONS","NYXOTE","NYXOUS","NYXURE",

    // O
    "OBJECT","OFFICE","ONLINE","OPTION","ORANGE","OUTPUT","OXYGEN","OBEYED","OBSERV","OBTAIN",
    "OCCURS","OCEANS","ODDITY","OFFERS","OFFSET","OLIVES","ONIONS","OPPOSE","OPTICS","ORIENT",

    // P
    "PLAYER","POCKET","POLICE","PUBLIC","PYTHON","PAINTS","PEOPLE","POWERS","PRISON","PREFER",
    "PRIZES","PROFIT","PROVEN","PUZZLE","PURITY","PURPLE","PURSUY","PUSHED","PUSHES","PUTTER",

    // Q
    "QUOTES","QUAKER","QUARTS","QUEENS","QUENCH","QUESTS","QUICKS","QUIETS","QUILLS","QUINTS",
    "QUORUM","QUOTED","QUOTER","QUOTES","QUOTUM","QUBITS","QUIDDY","QUIRES","QUIRKS","QUIVER",

    // R
    "RANDOM","REPORT","RESULT","RETURN","ROBOTS","RACING","RADIOS","RAISED","RAISES","RANGES",
    "RANKED","RANKER","RAPIDS","RASHLY","RATION","RATTED","RATTLE","RAVENS","READER","REMOTE",

    // S
    "SCREEN","SEARCH","SELECT","SERVER","SHADOW","SIGNAL","SIMPLE","SKILLS","SMILES","SOCIAL",
    "SOURCE","SPIRIT","SPORTS","SPRING","STATUS","STREAM","STREET","STRONG","SYSTEM","SCHOOL",

    // T
    "TARGET","TICKET","TRAVEL","TUNNEL","TABLES","TAKING","TALENT","TALKED","TALLER","TAMING",
    "TAPPED","TAPPER","TAROTS","TASTED","TASTES","TATTED","TATTLE","TAUGHT","TAXING","TEACHR",

    // U
    "UNIQUE","UPDATE","UPLIFT","UPPING","UPSHOT","UPWARD","URGENT","USABLE","USAGES","USEFUL",
    "USUALL","UTMOST","UTOPIA","UTTERS","UVULAS","UXORIC","UZBEKS","UZYONS","UZYOTE","UZYURE",

    // V
    "VISION","VOICES","VACANT","VACUUM","VAINLY","VALLEY","VALUED","VALUES","VANISH","VARIED",
    "VARIES","VECTOR","VELVET","VENUES","VERBAL","VERIFY","VERSUS","VESSEL","VIABLE","VIBRAN",

    // W
    "WALKER","WARMER","WATERS","WEALTH","WINTER","WORKER","WRITER","WACKYS","WADDLE","WAFFLE",
    "WAGING","WAGONS","WAISTS","WAITED","WAITER","WAIVES","WAKING","WALLED","WALLET","WANDER",

    // X
    "XENONS","XENIAS","XENIUM","XEROXY","XERONS","XEROTE","XEROTH","XERUMS","XETONS","XEZONS",
    "XIMENA","XINING","XIPHON","XIRONS","XITONS","XIZONS","XOBOTS","XOCOTE","XODONS","XOMICS",

    // Y
    "YELLOW","YIELDS","YOUNGS","YOUTHS","YAWNED","YAWNER","YEARLY","YEASTS","YELLED","YELLER",
    "YELLOW","YIELDS","YOUNGS","YOUTHS","YAWNED","YAWNER","YEARLY","YEASTS","YELLED","YELLER",

    // Z
    "ZEBRAS","ZEALOT","ZENITH","ZEROES","ZEROTH","ZESTED","ZESTER","ZIPPED","ZIPPER","ZODIAC",
    "ZOMBIE","ZONING","ZOOLOG","ZORROS","ZOSTER"
    };

    public static readonly HashSet<string> FilipinoWords = new HashSet<string>
    {
        "ABANTE","ABISIN","ABOGAD","ABULAN","ADARNA","AGIMAT","AGWAIN","ALAMIN","ALAPAG","ALIPIN",
        "ALONAN","AMANTE","AMIHAN","ANAKAN","ANGHEL","ANIMAS","ANTOKA","APATAN","APOYAN","ARAWAN",
        "ASINAN","ASWANG","ATIPAN","AWITAN","AYAWAN","BABALA","BABOYI","BAGONG","BAHAGI","BAKUNA",
        "BALANG","BALITA","BALONG","BANDIL","BANLIG","BANTAY","BARANG","BARKAD","BASURA","BATANG",
        "BATERY","BAYANI","BAYBAY","BAYONG","BILANG","BINAGO","BINATA","BISITA","BITUIN","BUHAYI",
        "BUKANA","BUKIRI","BULALO","BULONG","BUNGAI","BUNSOY","BURADO","BUSINA","BUTIKI","CABANA",
        "CANTON","CARLOS","CEBUAN","DALAGA","DALANG","DALIRI","DAMDAM","DANGAL","DAPITH","DAYOYO",
        "DEKADA","DELATA","DELUBO","DILAWN","DINGAS","DIPANG","DIYOSA","DUGONG","DULANG","DUMALO",
        "DUMARA","DUMAYO","DUNONG","DURUGO","EKSENA","ELERON","ERMITA","ESKOLA","ESTILO","ETNIKO",
        "FIESTA","FILIPN","GABAYI","GABING","GALING","GAMOTA","GANDAN","GANITO","GANYAN","GARAPO",
        "GARDEN","GASOLI","GATONG","GAYUMA","GINAWA","GINITO","GINOON","GINUGO","GISING","GITARA",
        "GUBATN","GUHITN","GULAYI","GULONG","GUMAWA","GUNITA","HABANG","HAGDAN","HALAGA","HALILI",
        "HALINA","HAMOGN","HANAPN","HANDAI","HANGIN","HAPLOS","HARANA","HARING","HATING","HIBANG",
        "HIGPIT","HILAGA","HIMALA","HIMIGI","HINGAN","HIRAYA","HULING","HUMAYO","HUNING","HUSAYI",
        "IBIGAN","ILAWAN","ILIGAN","ILOGAN","IMBAKA","IMBUDO","INAPOY","INAYAN","INIPON","INOMAN",
        "IPINAG","IPINAS","ISIPAN","ITANON","ITAPON","ITLOGN","IWASAN","JAMBOG","JAROON","JULIUS",
        "KABATA","KABILA","KABITA","KADENA","KAGAWA","KAHARI","KAHINA","KAILAN","KAISIP","KALAHI",
        "KALANG","KALAYA","KALIPI","KALOOB","KAMADA","KAMBAL","KAMINA","KAMOTE","KANINA","KANYON",
        "KAPANA","KAPANG","KAPITA","KARANI","KARINA","KARITA","KASAMA","KASAPI","KASAYA","KASILA",
        "KASINA","KATANA","KAWALA","KAWALI","KAWANI","KAYANI","KAYASA","LAKASA","LAKILA","LAMANA",
        "LAMANI","LAMISA","LANISA","LANITA","LAPISA","LAPITA","LARISA","LARITA","LASISA","LASITA",
        "LATISA","LATITA","LAWANI","LAWITA","LAYANI","LAYITA","LEKISA","LEKITA","LIMANI","LIMITA",
        "LINISA","LINITA","LIPISA","LIPITA","LIRISA","LIRITA","LISISA","LISITA","LITISA","LITITA",
        "LOKISA","LOKITA","LONISA","LONITA","LUPISA","LUPITA","MAHALO","MAHINA","MAKATA","MAKINA",
        "MAKITA","MALAYA","MALINA","MALISA","MALITA","MAMAYA","MANALO","MANILA","MANISA","MANITA",
        "MAPISA","MAPITA","MARINA","MARISA","MARITA","MASAYA","MATISA","MATITA","MAYANI","MAYISA",
        "MEKISA","MEKITA","MELISA","MELITA","MENISA","MENITA","MERISA","MERITA","MESISA","MESITA",
        "MIKISA","MIKITA","MINISA","MINITA","MIRISA","MIRITA","MISISA","MISITA","MITISA","MITITA"
    };
    public void OnGet()
    {
        var data = HttpContext.Session.GetString("Guesses");
        if (data != null)
        {
            Guesses = JsonSerializer.Deserialize<List<string>>(data);
        }

        var outcomes = HttpContext.Session.GetString("Outcomes");
        if (outcomes != null)
        {
            Outcomes = JsonSerializer.Deserialize<List<string>>(outcomes);
        }

        var target = HttpContext.Session.GetString("TargetWord");
        if (target != null)
        {
            Target = JsonSerializer.Deserialize<string>(target);
        }
        else
        {
            GenerateTargetWord();
        }
    
    }

    public IActionResult OnPost()
    {
        var data = HttpContext.Session.GetString("Guesses");
        if (data != null)
        {
            Guesses = JsonSerializer.Deserialize<List<string>>(data);
        }

        var target = HttpContext.Session.GetString("TargetWord");
        if (target != null)
        {
            Target = JsonSerializer.Deserialize<string>(target);
        }

        var outcomes = HttpContext.Session.GetString("Outcomes");
        if (outcomes != null)
        {
            Outcomes = JsonSerializer.Deserialize<List<string>>(outcomes);
        }

        if (!string.IsNullOrEmpty(Guess) && Guess.Length == 6 && Guesses.Count < 6)
        {
            Guess = Guess.ToUpper();

            if (FilipinoWords.Contains(Guess))
            {
                Guesses.Add(Guess);
                GuessChecker();
            } else
            {
                ModelState.AddModelError("Guess", "That word it not valid");
            }

            this.Guess = string.Empty;
        } else
        {
            ModelState.AddModelError("Guess", "That is not a 6 letter word");
        }

        HttpContext.Session.SetString("Guesses", JsonSerializer.Serialize(Guesses));
        HttpContext.Session.SetString("Outcomes", JsonSerializer.Serialize(Outcomes));

        if (!ModelState.IsValid)
        {
            return Page(); // stays on POST, shows error
        }
        return RedirectToPage(); // normal flow
    }

    public IActionResult OnPostNewGame()
    {
        Guesses.Clear();
        HttpContext.Session.Remove("Guesses");
        HttpContext.Session.Remove("Outcomes");
        GenerateTargetWord();
        return RedirectToPage();
    }

    public void GenerateTargetWord()
    {
        var random = new Random();
        Target = FilipinoWords.ElementAt(random.Next(FilipinoWords.Count));
        // Target = "ABANTE";
        HttpContext.Session.SetString("TargetWord", JsonSerializer.Serialize(Target));
    }

    public void GuessChecker()
    {
        List<int> indexToSkip = [];
        List<string> outcome = [];

        // Iteration for the letters of the user's guess
        for(int i = 0; i < Guess.Length; i++)
        {
            // Iteration for the letters of the target word
            for(int j = 0; j < Target.Length; j++)
            {
                if (!indexToSkip.Contains(j))
                {
                    if (Guess[i].Equals(Target[j]))
                    {
                        outcome.Add(i == j ? "green" : "yellow");
                        indexToSkip.Add(j);
                        break;
                    } 
                }
            }

            if(outcome.Count == i)
            {
                outcome.Add("gray");
            }
        }
        outcome.ForEach(o =>
        {
           Outcomes.Add(o); 
        });

        if(!(outcome.Contains("gray") || outcome.Contains("yellow")))
        {
            Target = $"Congratulations, \nyou have guessed the word in {Outcomes.Count / 6} attempt/s!!";
            HttpContext.Session.SetString("TargetWord", JsonSerializer.Serialize(Target));
        } else if(Guesses.Count >= 6)
        {
            Target = $"The target word is '{Target}'\ntake on another challenge!!";
            HttpContext.Session.SetString("TargetWord", JsonSerializer.Serialize(Target));

        }
    }
}