namespace Zutatensuppe.DiabloInterface.Gui.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;

    using Zutatensuppe.D2Reader;
    using Zutatensuppe.D2Reader.Models;
    using Zutatensuppe.DiabloInterface.Business.Services;
    using Zutatensuppe.DiabloInterface.Business.Settings;
    using Zutatensuppe.DiabloInterface.Core.Extensions;
    using Zutatensuppe.DiabloInterface.Core.Logging;

    public abstract partial class AbstractLayout : UserControl
    {
        static readonly ILogger Logger = LogServiceLocator.Get(MethodBase.GetCurrentMethod().DeclaringType);

        readonly ISettingsService settingsService;
        readonly IGameService gameService;

        protected bool realFrwIas;
        GameDifficulty? activeDifficulty;
        CharacterClass? activeCharacterClass;

        protected IEnumerable<Label> InfoLabels { get; set; }
        protected IEnumerable<Label> FireLabels { get; set; }
        protected IEnumerable<Label> ColdLabels { get; set; }
        protected IEnumerable<Label> LighLabels { get; set; }
        protected IEnumerable<Label> PoisLabels { get; set; }
        protected IEnumerable<Label> BaseStatLabels { get; set; }
        protected IEnumerable<Label> AdvancedStatLabels { get; set; }
        protected IEnumerable<Label> ResistanceLabels { get; set; }
        protected IEnumerable<Label> DifficultyLabels { get; set; }

        protected IEnumerable<FlowLayoutPanel> RunePanels { get; set; }

        protected Dictionary<Control, string> DefaultTexts { get; set; }

        protected abstract Panel RuneLayoutPanel { get; }

        protected FlowLayoutPanel outerLeftRightPanel;
        protected Label nameLabel;
        protected Label playersXLabel;
        protected Label deathsLabel;
        protected Label deathsLabelVal;
        protected Label lvlLabel;
        protected Label lvlLabelVal;
        protected Label goldLabel;
        protected Label goldLabelVal;
        protected Label magicFindLabel;
        protected Label magicFindLabelVal;
        protected Label monsterGoldLabel;
        protected Label monsterGoldLabelVal;
        protected Label attackerSelfDamageLabel;
        protected Label attackerSelfDamageLabelVal;
        protected TableLayoutPanel panelBaseStats;
        protected Label vitLabel;
        protected Label labelVitVal;
        protected Label dexLabel;
        protected Label labelDexVal;
        protected Label strLabel;
        protected Label labelStrVal;
        protected Label eneLabel;
        protected Label labelEneVal;
        protected TableLayoutPanel panelAdvancedStats;
        protected Label iasLabel;
        protected Label frwLabel;
        protected Label fcrLabel;
        protected Label fhrLabel;
        protected Label labelFrwVal;
        protected Label labelFhrVal;
        protected Label labelFcrVal;
        protected Label labelIasVal;
        protected TableLayoutPanel panelResistances;
        protected Label coldLabel;
        protected Label lighLabel;
        protected Label poisLabel;
        protected Label fireLabel;
        protected Label labelFireResVal;
        protected Label labelLightResVal;
        protected Label labelPoisonResVal;
        protected Label labelColdResVal;
        protected TableLayoutPanel panelDiffPercentages;
        protected Label normLabel;
        protected Label nmLabel;
        protected Label hellLabel;
        protected Label normLabelVal;
        protected Label nmLabelVal;
        protected Label hellLabelVal;
        protected Label labelNormPerc;
        protected Label labelNmPerc;
        protected Label labelHellPerc;
        protected TableLayoutPanel panelDeathsLvl;
        protected TableLayoutPanel panelSimpleStats;
        protected FlowLayoutPanel panelStats;
        protected FlowLayoutPanel panelDiffPercentages2;
        protected FlowLayoutPanel panelRuneDisplayHorizontal;
        protected FlowLayoutPanel panelRuneDisplayVertical;
        protected Label gameCounterLabel;
        protected Label gameCounterLabelVal;

        protected void InitializeComponent()
        {
            this.outerLeftRightPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panelRuneDisplayVertical = new System.Windows.Forms.FlowLayoutPanel();
            this.nameLabel = new System.Windows.Forms.Label();
            this.playersXLabel = new System.Windows.Forms.Label();
            this.panelDeathsLvl = new System.Windows.Forms.TableLayoutPanel();
            this.deathsLabel = new System.Windows.Forms.Label();
            this.deathsLabelVal = new System.Windows.Forms.Label();
            this.lvlLabel = new System.Windows.Forms.Label();
            this.lvlLabelVal = new System.Windows.Forms.Label();
            this.panelSimpleStats = new System.Windows.Forms.TableLayoutPanel();
            this.goldLabel = new System.Windows.Forms.Label();
            this.goldLabelVal = new System.Windows.Forms.Label();
            this.magicFindLabel = new System.Windows.Forms.Label();
            this.magicFindLabelVal = new System.Windows.Forms.Label();
            this.monsterGoldLabel = new System.Windows.Forms.Label();
            this.monsterGoldLabelVal = new System.Windows.Forms.Label();
            this.attackerSelfDamageLabel = new System.Windows.Forms.Label();
            this.attackerSelfDamageLabelVal = new System.Windows.Forms.Label();
            this.panelStats = new System.Windows.Forms.FlowLayoutPanel();
            this.panelBaseStats = new System.Windows.Forms.TableLayoutPanel();
            this.vitLabel = new System.Windows.Forms.Label();
            this.dexLabel = new System.Windows.Forms.Label();
            this.strLabel = new System.Windows.Forms.Label();
            this.eneLabel = new System.Windows.Forms.Label();
            this.labelStrVal = new System.Windows.Forms.Label();
            this.labelDexVal = new System.Windows.Forms.Label();
            this.labelVitVal = new System.Windows.Forms.Label();
            this.labelEneVal = new System.Windows.Forms.Label();
            this.panelAdvancedStats = new System.Windows.Forms.TableLayoutPanel();
            this.iasLabel = new System.Windows.Forms.Label();
            this.frwLabel = new System.Windows.Forms.Label();
            this.fcrLabel = new System.Windows.Forms.Label();
            this.fhrLabel = new System.Windows.Forms.Label();
            this.labelFrwVal = new System.Windows.Forms.Label();
            this.labelFhrVal = new System.Windows.Forms.Label();
            this.labelFcrVal = new System.Windows.Forms.Label();
            this.labelIasVal = new System.Windows.Forms.Label();
            this.panelResistances = new System.Windows.Forms.TableLayoutPanel();
            this.coldLabel = new System.Windows.Forms.Label();
            this.lighLabel = new System.Windows.Forms.Label();
            this.poisLabel = new System.Windows.Forms.Label();
            this.fireLabel = new System.Windows.Forms.Label();
            this.labelFireResVal = new System.Windows.Forms.Label();
            this.labelLightResVal = new System.Windows.Forms.Label();
            this.labelPoisonResVal = new System.Windows.Forms.Label();
            this.labelColdResVal = new System.Windows.Forms.Label();
            this.panelDiffPercentages = new System.Windows.Forms.TableLayoutPanel();
            this.normLabel = new System.Windows.Forms.Label();
            this.nmLabel = new System.Windows.Forms.Label();
            this.hellLabel = new System.Windows.Forms.Label();
            this.normLabelVal = new System.Windows.Forms.Label();
            this.nmLabelVal = new System.Windows.Forms.Label();
            this.hellLabelVal = new System.Windows.Forms.Label();
            this.panelDiffPercentages2 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelNormPerc = new System.Windows.Forms.Label();
            this.labelNmPerc = new System.Windows.Forms.Label();
            this.labelHellPerc = new System.Windows.Forms.Label();
            this.panelRuneDisplayHorizontal = new System.Windows.Forms.FlowLayoutPanel();
            this.gameCounterLabel = new System.Windows.Forms.Label();
            this.gameCounterLabelVal = new System.Windows.Forms.Label();

            this.outerLeftRightPanel.SuspendLayout();
            this.panelDeathsLvl.SuspendLayout();
            this.panelSimpleStats.SuspendLayout();
            this.panelStats.SuspendLayout();
            this.panelBaseStats.SuspendLayout();
            this.panelAdvancedStats.SuspendLayout();
            this.panelResistances.SuspendLayout();
            this.panelDiffPercentages.SuspendLayout();
            this.panelDiffPercentages2.SuspendLayout();
        }

        protected AbstractLayout(ISettingsService settingsService, IGameService gameService)
        {
            this.settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
            this.gameService = gameService ?? throw new ArgumentNullException(nameof(gameService));

            RegisterServiceEventHandlers();
            InitializeComponent();
            InitializeElements();

            Load += (sender, e) => UpdateSettings(settingsService.CurrentSettings);

            // Clean up events when disposed because services outlive us.
            Disposed += (sender, e) => UnregisterServiceEventHandlers();
        }

        void InitializeElements()
        {
            InfoLabels = Enumerable.Empty<Label>();
            FireLabels = Enumerable.Empty<Label>();
            ColdLabels = Enumerable.Empty<Label>();
            LighLabels = Enumerable.Empty<Label>();
            PoisLabels = Enumerable.Empty<Label>();
            BaseStatLabels = Enumerable.Empty<Label>();
            AdvancedStatLabels = Enumerable.Empty<Label>();
            DifficultyLabels = Enumerable.Empty<Label>();

            RunePanels = Enumerable.Empty<FlowLayoutPanel>();
            DefaultTexts = new Dictionary<Control, string>();
        }

        void RegisterServiceEventHandlers()
        {
            settingsService.SettingsChanged += SettingsServiceOnSettingsChanged;
            gameService.CharacterCreated += GameServiceOnCharacterCreated;
            gameService.DataRead += GameServiceOnDataRead;
        }

        void UnregisterServiceEventHandlers()
        {
            settingsService.SettingsChanged -= SettingsServiceOnSettingsChanged;

            gameService.CharacterCreated -= GameServiceOnCharacterCreated;
            gameService.DataRead -= GameServiceOnDataRead;
        }

        void SettingsServiceOnSettingsChanged(object sender, ApplicationSettingsEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke((Action)(() => SettingsServiceOnSettingsChanged(sender, e)));
                return;
            }

            activeCharacterClass = null;

            UpdateSettings(e.Settings);
        }

        void GameServiceOnCharacterCreated(object sender, CharacterCreatedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke((Action)(() => GameServiceOnCharacterCreated(sender, e)));
                return;
            }

            Reset();
        }

        void GameServiceOnDataRead(object sender, DataReadEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke((Action)(() => GameServiceOnDataRead(sender, e)));
                return;
            }

            UpdateLabels(e.Character, e.Quests, e.CurrentPlayersX, e.GameCounter);
            UpdateClassRuneList(e.Character.CharClass);
            UpdateRuneDisplay(e.ItemIds);
        }

        public void Reset()
        {
            foreach (FlowLayoutPanel fp in RunePanels)
            {
                if (fp.Controls.Count <= 0)
                    continue;

                foreach (RuneDisplayElement c in fp.Controls)
                {
                    c.SetHaveRune(false);
                }
            }

            foreach (KeyValuePair<Control, string> pair in DefaultTexts)
            {
                pair.Key.Text = pair.Value;
            }
        }
        
        protected void UpdateSettings(ApplicationSettings settings)
        {
            ApplyLabelSettings(settings);
            ApplyRuneSettings(settings);
            UpdateLayout(settings);
        }

        virtual protected void ApplyLabelSettings(ApplicationSettings settings)
        {
            realFrwIas = settings.DisplayRealFrwIas;
            BackColor = settings.ColorBackground;

            nameLabel.Font = new Font(settings.FontName, settings.FontSizeTitle);
            var infoFont = new Font(settings.FontName, settings.FontSize);
            foreach (Label label in InfoLabels)
            {
                label.Font = infoFont;
            }

            // Hide/show labels wanted labels.
            nameLabel.Visible = settings.DisplayName;
            playersXLabel.Visible = settings.DisplayPlayersX;

            goldLabel.Visible = settings.DisplayGold;
            goldLabelVal.Visible = settings.DisplayGold;
            monsterGoldLabel.Visible = settings.DisplayMonsterGold;
            monsterGoldLabelVal.Visible = settings.DisplayMonsterGold;
            magicFindLabel.Visible = settings.DisplayMagicFind;
            magicFindLabelVal.Visible = settings.DisplayMagicFind;
            attackerSelfDamageLabel.Visible = settings.DisplayAttackerSelfDamage;
            attackerSelfDamageLabelVal.Visible = settings.DisplayAttackerSelfDamage;
            deathsLabel.Visible = settings.DisplayDeathCounter;
            deathsLabelVal.Visible = settings.DisplayDeathCounter;
            lvlLabel.Visible = settings.DisplayLevel;
            lvlLabelVal.Visible = settings.DisplayLevel;
            gameCounterLabel.Visible = settings.DisplayGameCounter;
            gameCounterLabelVal.Visible = settings.DisplayGameCounter;
        }

        abstract protected void ApplyRuneSettings(ApplicationSettings settings);

        abstract protected void UpdateLayout(ApplicationSettings settings);

        protected void UpdateLabels(Character player, IList<QuestCollection> quests, int currentPlayersX, uint gameIndex)
        {
            nameLabel.Text = player.Name;
            playersXLabel.Text = "/players " + currentPlayersX;

            lvlLabelVal.Text = "" + player.Level;
            goldLabelVal.Text = "" + (player.Gold + player.GoldStash);
            deathsLabelVal.Text = "" + player.Deaths;
            labelStrVal.Text = "" + player.Strength;
            labelDexVal.Text = "" + player.Dexterity;
            labelVitVal.Text = "" + player.Vitality;
            labelEneVal.Text = "" + player.Energy;
            UpdateLabelWidthAlignment(labelStrVal, labelDexVal, labelVitVal, labelEneVal);

            labelFrwVal.Text = "" + (realFrwIas ? player.RealFRW() : player.FasterRunWalk);
            labelFcrVal.Text = "" + player.FasterCastRate;
            labelFhrVal.Text = "" + player.FasterHitRecovery;
            labelIasVal.Text = "" + (realFrwIas ? player.RealIAS() : player.IncreasedAttackSpeed);
            UpdateLabelWidthAlignment(labelFrwVal, labelFcrVal, labelFhrVal, labelIasVal);

            labelFireResVal.Text = "" + player.FireResist;
            labelColdResVal.Text = "" + player.ColdResist;
            labelLightResVal.Text = "" + player.LightningResist;
            labelPoisonResVal.Text = "" + player.PoisonResist;
            UpdateLabelWidthAlignment(labelFireResVal, labelColdResVal, labelLightResVal, labelPoisonResVal);

            IList<float> completions = quests.Select(q => q.CompletionProgress).ToList();

            normLabelVal.Text = $@"{completions[0]:0%}";
            nmLabelVal.Text = $@"{completions[1]:0%}";
            hellLabelVal.Text = $@"{completions[2]:0%}";
            UpdateLabelWidthAlignment(normLabelVal, nmLabelVal, hellLabelVal);

            labelNormPerc.Text = $@"NO: {completions[0]:0%}";
            labelNmPerc.Text = $@"NM: {completions[1]:0%}";
            labelHellPerc.Text = $@"HE: {completions[2]:0%}";

            gameCounterLabelVal.Text = "" + gameIndex;
            monsterGoldLabelVal.Text = "" + player.MonsterGold;
            magicFindLabelVal.Text = "" + player.MagicFind;
            attackerSelfDamageLabelVal.Text = "" + player.AttackerSelfDamage;
        }

        protected void UpdateLabelWidthAlignment(params Label[] labels)
        {
            var maxWidth = 0;

            foreach (var label in labels)
            {
                var measuredSize = TextRenderer.MeasureText(label.Text, label.Font, Size.Empty, TextFormatFlags.SingleLine);
                maxWidth = Math.Max(measuredSize.Width, maxWidth);
            }

            foreach (var label in labels)
            {
                label.MinimumSize = new Size(maxWidth, 0);
            }
        }

        void UpdateClassRuneList(CharacterClass characterClass)
        {
            var settings = settingsService.CurrentSettings;
            if (!settings.DisplayRunes) return;

            var targetDifficulty = gameService.TargetDifficulty;
            var isCharacterClassChanged = activeCharacterClass == null || activeCharacterClass != characterClass;
            var isGameDifficultyChanged = activeDifficulty != targetDifficulty;

            if (!isCharacterClassChanged && !isGameDifficultyChanged)
                return;

            Logger.Info("Loading rune list.");
            
            var runeSettings = GetMostSpecificRuneSettings(characterClass, targetDifficulty);
            UpdateRuneList(settings, runeSettings?.Runes?.ToList());

            activeDifficulty = targetDifficulty;
            activeCharacterClass = characterClass;
        }

        void UpdateRuneList(ApplicationSettings settings, IReadOnlyList<Rune> runes)
        {
            var panel = RuneLayoutPanel;
            if (panel == null) return;

            panel.Controls.ClearAndDispose();
            panel.Visible = runes?.Count > 0;
            runes?.ForEach(rune => panel.Controls.Add(
                new RuneDisplayElement(rune, settings.DisplayRunesHighContrast, false, false)));
        }

        /// <summary>
        /// Gets the most specific rune settings in the order:
        ///     Class+Difficulty > Class > Difficulty > None
        /// </summary>
        /// <param name="characterClass">Active character class.</param>
        /// <param name="targetDifficulty">Manual difficulty selection.</param>
        /// <returns>The rune settings.</returns>
        ClassRuneSettings GetMostSpecificRuneSettings(CharacterClass characterClass, GameDifficulty targetDifficulty)
        {
            IEnumerable<ClassRuneSettings> runeClassSettings = settingsService.CurrentSettings.ClassRunes.ToList();
            return runeClassSettings.FirstOrDefault(rs => rs.Class == characterClass && rs.Difficulty == targetDifficulty)
                ?? runeClassSettings.FirstOrDefault(rs => rs.Class == characterClass && rs.Difficulty == null)
                ?? runeClassSettings.FirstOrDefault(rs => rs.Class == null && rs.Difficulty == targetDifficulty)
                ?? runeClassSettings.FirstOrDefault(rs => rs.Class == null && rs.Difficulty == null);
        }

        void UpdateRuneDisplay(IEnumerable<int> itemIds)
        {
            var panel = RuneLayoutPanel;
            if (panel == null) return;

            // Count number of items of each type.
            Dictionary<int, int> itemClassCounts = itemIds
                .GroupBy(id => id)
                .ToDictionary(g => g.Key, g => g.Count());

            foreach (RuneDisplayElement runeElement in panel.Controls)
            {
                var itemClassId = (int)runeElement.Rune + 610;

                if (itemClassCounts.ContainsKey(itemClassId) && itemClassCounts[itemClassId] > 0)
                {
                    itemClassCounts[itemClassId]--;
                    runeElement.SetHaveRune(true);
                }
            }
        }

        protected void UpdateLabelColors(ApplicationSettings settings)
        {
            nameLabel.ForeColor = settings.ColorName;
            playersXLabel.ForeColor = settings.ColorPlayersX;

            goldLabel.ForeColor = settings.ColorGold;
            goldLabelVal.ForeColor = settings.ColorGold;
            deathsLabel.ForeColor = settings.ColorDeaths;
            deathsLabelVal.ForeColor = settings.ColorDeaths;
            lvlLabel.ForeColor = settings.ColorLevel;
            lvlLabelVal.ForeColor = settings.ColorLevel;
            gameCounterLabel.ForeColor = settings.ColorGameCounter;
            gameCounterLabelVal.ForeColor = settings.ColorGameCounter;
            monsterGoldLabel.ForeColor = settings.ColorMonsterGold;
            monsterGoldLabelVal.ForeColor = settings.ColorMonsterGold;
            magicFindLabel.ForeColor = settings.ColorMagicFind;
            magicFindLabelVal.ForeColor = settings.ColorMagicFind;
            attackerSelfDamageLabel.ForeColor = settings.ColorAttackerSelfDamage;
            attackerSelfDamageLabelVal.ForeColor = settings.ColorAttackerSelfDamage;

            foreach (Label l in FireLabels)
                l.ForeColor = settings.ColorFireRes;

            foreach (Label l in ColdLabels)
                l.ForeColor = settings.ColorColdRes;

            foreach (Label l in LighLabels)
                l.ForeColor = settings.ColorLightningRes;

            foreach (Label l in PoisLabels)
                l.ForeColor = settings.ColorPoisonRes;

            foreach (Label l in BaseStatLabels)
                l.ForeColor = settings.ColorBaseStats;

            foreach (Label l in AdvancedStatLabels)
                l.ForeColor = settings.ColorAdvancedStats;

            foreach (Label l in DifficultyLabels)
                l.ForeColor = settings.ColorDifficultyPercentages;
        }

        private Size MeasureText(string str, Control control)
        {
            return TextRenderer.MeasureText(str, control.Font, Size.Empty, TextFormatFlags.SingleLine);
        }

        private Size MeasureText(string str) => MeasureText(str, strLabel);

        protected Size MeasureNameSize() => MeasureText(new string('W', 15), nameLabel);

        protected Size MeasureGoldSize() => MeasureText("GOLD: 2500000");

        protected Size MeasureDeathsSize() => MeasureText("DEATHS: 99");

        protected Size MeasureLvlSize() => MeasureText("LVL: 99");

        // base stats have 3 char label (STR, VIT, ect.) and realistically
        // a max value < 500 (lvl 99*5 + alkor quest... items can increase this tho)
        // we will assume the "longest" string is DEX: 499
        // (most likely dex or ene will be longest str.)
        protected Size MeasureBaseStatsSize() => MeasureText("DEX: 499");

        // advanced stats have 3 char label (FCR, FRW, etc.) and realistically
        // a max value of slightly over 100
        // we will assume the "longest" string is FRW: 999
        protected Size MeasureAdvancedStatsSize() => MeasureText("FRW: 999");

        // Panel size for resistances can be negative, so max number of chars
        // are 10 (LABL: -VAL) resistances never go below -100 (longest possible
        // string for the label) and never go above 95 we will assume the
        // "longest" string is COLD: -100
        protected Size MeasureResistancesSize() => MeasureText("COLD: -100");

        // we will assume the "longest" string is NORM: 100%
        protected Size MeasureDifficultyPercentageSize() => MeasureText("NORM: 100%");
    }
}
