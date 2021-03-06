﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WMPLib;

namespace speechRecoLib
{
    /// <summary>
    /// <para>Class to manage Command. This is a terminal node.</para>
    /// <para>Words is for building a regex with a list of word. The order is imporant.</para>
    /// <para>Command is a simple command. It's used for the grammar.</para>
    /// <para>Action is an object that handle command's action.</para>
    /// <para>Answer will be spoken by your computer.</para>
    /// <para>Play is the name of the song to be played.</para>
    /// </summary>
    public class Command : CommandManager
    {
        /// <summary>
        /// To play an mp3. Static to avoid overlapping.
        /// </summary>
        static WMPLib.WindowsMediaPlayer _wplayer = new WMPLib.WindowsMediaPlayer();

        /// <summary>
        /// Object Action. The Command exec this _action
        /// </summary>
        public ActionManager _action { get; set; }
        /// <summary>
        /// Answer for text2speech
        /// </summary>
        public string _answer { get; set; }
        /// <summary>
        /// mp3 to be played
        /// </summary>
        public string _play { get; set; }

        /// <summary>
        /// Constructor.
        /// Default.
        /// </summary>
        public Command() : base()
        {
        
        }

        /// <summary>
        /// Constructor.
        /// List of words and action.
        /// </summary>
        /// <param name="words">List of words for the regex. ORDER VERY IMPORTANT</param>
        /// <param name="action">Action to perform</param>
        public Command(List<string> words, ActionManager action) : base(words)
        {
            _action = action;
        }

        /// <summary>
        /// Constructor.
        /// Command and action
        /// </summary>
        /// <param name="command">Command for the grammar</param>
        /// <param name="action">Action to perform</param>
        public Command(string command, ActionManager action) : base(command)
        {
            _action = action;
        }

        /// <summary>
        /// Constructor.
        /// List of word, command and action.
        /// </summary>
        /// <param name="words">List of words for the regex. ORDER VERY IMPORTANT</param>
        /// <param name="command">Command for the grammar</param>
        /// <param name="action">Action to perform</param>
        public Command(List<string> words, string command, ActionManager action) : base(words, command)
        {
            _action = action;
        }

        /// <summary>
        /// Constructor.
        /// List of word, command, answer and action.
        /// </summary>
        /// <param name="words">List of words for the regex. ORDER VERY IMPORTANT</param>
        /// <param name="command">Command for the grammar</param>
        /// <param name="answer">Answer for text2speech</param>
        /// <param name="action">Action to perform</param>
        public Command(List<string> words, string command, string answer, ActionManager action)  : base(words, command)
        {
            _answer = answer;
            _action = action;
        }

        /// <summary>
        /// Constructor.
        /// List of word, command, answer, song to play and action.
        /// </summary>
        /// <param name="words">List of words for the regex. ORDER VERY IMPORTANT</param>
        /// <param name="command">Command for the grammar</param>
        /// <param name="answer">Answer for text2speech</param>
        /// <param name="play">mp3 to be played</param>
        /// <param name="action">Action to perform</param>
        public Command(List<string> words, string command , string answer, string play, ActionManager action)  : base(words, command)
        {
            _answer = answer;
            _play = play;
            _action = action;
        }

        /// <summary>
        /// Play the mp3 file loaded.
        /// </summary>
        public void Play() 
        {
            _wplayer.URL = _play;
            _wplayer.controls.play();
        }

        /// <summary>
        /// Stop the player.
        /// </summary>
        public void Stop()
        {
            _wplayer.controls.stop();
        }

        /// <summary>
        /// Perform the action
        /// </summary>
        /// <returns>true is action performed</returns>
        protected override bool Perform(string text)
        {
            if (this.Matches(text))
            {
                if (_answer != null)
                {
                    SpeechManager.Speak(_answer);
                }

                if (_play != null)
                {
                    Play();
                }

                return _action.Perform();
            }

            return false;
        }

        protected override bool PerformFirst(string text)
        {
            if (this.Matches(text))
            {
                if (_answer != null)
                {
                    SpeechManager.Speak(_answer);
                }

                if (_play != null)
                {
                    Play();
                }

                return _action.Perform();
            }

            return false;
        }
    }
}
