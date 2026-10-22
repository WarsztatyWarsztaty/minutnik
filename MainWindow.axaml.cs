using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;

namespace Minutnik;

public partial class MainWindow : Window
{
    private readonly DispatcherTimer _timer;
    private bool _isRunning = false;
    private bool _isBreakMode = false;
    private int _remainingSeconds;
    private int _completedCycles = 0;

    public MainWindow()
    {
        InitializeComponent();

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += OnTimerTick;

        _remainingSeconds = GetWorkDurationSeconds();
        UpdateDisplay();
    }

    private int GetWorkDurationSeconds()
    {
        int minutes = (int)(WorkDurationInput?.Value ?? 25);
        if (minutes < 1) minutes = 1;
        return minutes * 60;
    }

    private int GetBreakDurationSeconds()
    {
        int minutes = (int)(BreakDurationInput?.Value ?? 5);
        if (minutes < 1) minutes = 1;
        return minutes * 60;
    }

    private void OnStartPauseClicked(object? sender, RoutedEventArgs e)
    {
        if (_isRunning)
        {
            PauseTimer();
        }
        else
        {
            StartTimer();
        }
    }

    private void StartTimer()
    {
        if (_remainingSeconds <= 0)
        {
            _remainingSeconds = _isBreakMode ? GetBreakDurationSeconds() : GetWorkDurationSeconds();
        }

        _isRunning = true;
        _timer.Start();

        if (StatusMessageTextBlock != null)
        {
            StatusMessageTextBlock.Text = _isBreakMode ? "Trwa czas przerwy..." : "Trwa czas pracy...";
        }

        UpdateDisplay();
    }

    private void PauseTimer()
    {
        _isRunning = false;
        _timer.Stop();

        if (StatusMessageTextBlock != null)
        {
            StatusMessageTextBlock.Text = "Minutnik wstrzymany.";
        }

        UpdateDisplay();
    }

    private void OnResetClicked(object? sender, RoutedEventArgs e)
    {
        _timer.Stop();
        _isRunning = false;
        _isBreakMode = false;
        _completedCycles = 0;
        _remainingSeconds = GetWorkDurationSeconds();

        if (StatusMessageTextBlock != null)
        {
            StatusMessageTextBlock.Text = "Minutnik zresetowany. Gotowy do pracy.";
        }

        UpdateDisplay();
    }

    private void OnDurationValueChanged(object? sender, NumericUpDownValueChangedEventArgs e)
    {
        if (TimeTextBlock == null || CycleCountTextBlock == null)
        {
            return;
        }

        // If timer is paused or stopped, update remaining time immediately
        if (!_isRunning)
        {
            _remainingSeconds = _isBreakMode ? GetBreakDurationSeconds() : GetWorkDurationSeconds();
            UpdateDisplay();
        }
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        if (_remainingSeconds > 0)
        {
            _remainingSeconds--;
            UpdateDisplay();
        }

        if (_remainingSeconds <= 0)
        {
            if (!_isBreakMode)
            {
                // Work session ended -> cycle count increments, break starts automatically
                _completedCycles++;
                _isBreakMode = true;
                _remainingSeconds = GetBreakDurationSeconds();

                if (StatusMessageTextBlock != null)
                {
                    StatusMessageTextBlock.Text = $"Koniec pracy! Ukończono cykl {_completedCycles}. Przerwa ruszyła automatycznie.";
                }

                UpdateDisplay();
                // Timer remains started, break counts down automatically
            }
            else
            {
                // Break session ended -> pause and prepare for the next work session
                _timer.Stop();
                _isRunning = false;
                _isBreakMode = false;
                _remainingSeconds = GetWorkDurationSeconds();

                if (StatusMessageTextBlock != null)
                {
                    StatusMessageTextBlock.Text = "Koniec przerwy! Naciśnij Start, aby rozpocząć kolejny cykl.";
                }

                UpdateDisplay();
            }
        }
    }

    private void UpdateDisplay()
    {
        if (TimeTextBlock == null || CycleCountTextBlock == null || StartPauseButton == null)
        {
            return;
        }

        // Time formatted as mm:ss
        int minutes = _remainingSeconds / 60;
        int seconds = _remainingSeconds % 60;
        TimeTextBlock.Text = $"{minutes:D2}:{seconds:D2}";

        // Cycle counter
        CycleCountTextBlock.Text = _completedCycles.ToString();

        // Start / Pause button state
        if (_isRunning)
        {
            StartPauseButton.Content = "Pauza";
            StartPauseButton.Background = Brush.Parse("#d97706"); // Amber
        }
        else
        {
            StartPauseButton.Content = "Start";
            StartPauseButton.Background = Brush.Parse("#2563eb"); // Blue
        }

        // Mode badge indicator
        if (ModeBadgeBorder != null && ModeBadgeTextBlock != null)
        {
            if (_isBreakMode)
            {
                ModeBadgeBorder.Background = Brush.Parse("#065f46"); // Green background
                ModeBadgeTextBlock.Text = "PRZERWA";
                ModeBadgeTextBlock.Foreground = Brush.Parse("#34d399"); // Bright green text
            }
            else
            {
                ModeBadgeBorder.Background = Brush.Parse("#1e3a8a"); // Blue background
                ModeBadgeTextBlock.Text = "PRACA";
                ModeBadgeTextBlock.Foreground = Brush.Parse("#60a5fa"); // Bright blue text
            }
        }
    }
}