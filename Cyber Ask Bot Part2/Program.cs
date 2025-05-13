using System;
using System.Collections.Generic;
using System.Media;
using System.Threading;
using System.IO;

class CyberAskBot
{
    internal class Program
    {
        static Dictionary<string, string> userMemory = new Dictionary<string, string>();
        static Dictionary<string, string> keywordResponses = new Dictionary<string, string>()
        {
            { "password", "Make sure to use strong, unique passwords for each account. Avoid using personal details." },
            { "scam", "Scammers often use social engineering. Always verify before clicking links or giving information." },
            { "privacy", "Protect your privacy by limiting what you share online and reviewing your account settings." }
        };

          static List<string> phishingTips = new List<string>()
        {
            "Don't trust unexpected emails asking for urgent action.",
            "Hover over links to see where they lead before clicking.",
            "Always double-check the sender’s email address."
        };

        static List<string> naturalReplies = new List<string>()
        {
            "That's interesting. Tell me more.",
            "Hmm, let me think...",
            "Good question. Here's something you should know:",
            "You're raising an important point."
        };

        static List<string> genericCyberResponses = new List<string>()
        {
            "Remember to keep your devices updated.",
            "Use two-factor authentication whenever possible.",
            "Avoid connecting to public Wi-Fi without a VPN.",
            "Backup your data regularly to avoid ransomware threats."
        };

        static HashSet<string> cybersecurityKeywords = new HashSet<string>()
        {
            "cyber", "hack", "malware", "phishing", "ransomware",
            "firewall", "breach", "password", "security", "privacy",
            "scam", "vpn", "antivirus", "update", "backup"
        };

        static string lastTopic = "";

        delegate void ChatResponse(string input);
        static ChatResponse responseDelegate = HandleUserInput;

          static void Main()
         {
            Console.Title = "Cybersecurity Awareness Bot";
            Console.ForegroundColor = ConsoleColor.Cyan;
            DisplayAsciiArt();
            PlayVoiceGreeting();
            Console.ResetColor();

            Console.Write("\nWhat's your name? ");
string userName = Console.ReadLine();
userMemory["name"] = userName;  // Store name in memory
Console.WriteLine($"\nHello {userName}, I'm here to help you stay safe online!\n");

            // Start conversation loop
            while (true)
            {
                Console.Write("\nAsk me something or type 'exit' to leave: ");
                string input = Console.ReadLine().ToLower();
                if (input == "exit")
                {
                    Console.WriteLine("\nGoodbye! Stay safe online!");
                    break;
                }

                responseDelegate(input);
            }
        }

          static void HandleUserInput(string input)
        {
            Random rnd = new Random();

            if (input.Contains("interested in "))
            {
                int startIndex = input.IndexOf("interested in ") + "interested in ".Length;
                if (startIndex < input.Length)
                {
                    string interest = input.Substring(startIndex).Trim();
                    userMemory["interest"] = interest;
                    SimulatedTyping($"Great! I'll remember that you're interested in {interest}.");
                    lastTopic = interest;
                    return;
                }
            }

            if (userMemory.ContainsKey("interest") && input.Contains("remind me"))
            {
                SimulatedTyping($"As someone interested in {userMemory["interest"]}, you might want to review your security settings.");
                return;
            }

            // Smarter name recall detection
            string[] nameQueries = {
                "what is my name", "what's my name", "who am i",
                "do you know my name", "can you remember my name", "tell me my name"
            };

            foreach (var phrase in nameQueries)
            {
                if (input.Contains(phrase))
                {
                    if (userMemory.ContainsKey("name"))
                        SimulatedTyping($"You're {userMemory["name"]}, of course!");
                    else
                        SimulatedTyping("Hmm, I don't seem to remember your name yet.");
                    return;
                }
            }

            if (input.Contains("worried") || input.Contains("scared"))
            {
                SimulatedTyping("It's okay to feel that way. Let's learn how to protect yourself online.");
                return;
            }

            if (input.Contains("frustrated"))
            {
                SimulatedTyping("Don't give up! Cybersecurity can be tricky, but you're making progress.");
                return;
            }

            if (input.Contains("phishing"))
            {
                SimulatedTyping(phishingTips[rnd.Next(phishingTips.Count)]);
                lastTopic = "phishing";
                return;
            }

            if (input.Contains("more") && lastTopic == "phishing")
            {
                SimulatedTyping("Here's another phishing tip: " + phishingTips[rnd.Next(phishingTips.Count)]);
                return;
            }

            foreach (var kvp in keywordResponses)
            {
                if (input.Contains(kvp.Key))
                {
                    SimulatedTyping($"{naturalReplies[rnd.Next(naturalReplies.Count)]} {kvp.Value}");
                    lastTopic = kvp.Key;
                    return;
                }
            }

            foreach (var word in cybersecurityKeywords)
            {
                if (input.Contains(word))
                {
                    SimulatedTyping($"{naturalReplies[rnd.Next(naturalReplies.Count)]} {genericCyberResponses[rnd.Next(genericCyberResponses.Count)]}");
                    return;
                }
            }

            SimulatedTyping(naturalReplies[rnd.Next(naturalReplies.Count)]);
        }

        static void SimulatedTyping(string message)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(30);
            }
            Console.WriteLine("\n");
        }


        static void DisplayAsciiArt()
    {
        Console.WriteLine(@"
       
                    $$\                                  $$$$$$\            $$\             $$$$$$$\            $$\     
                    $$ |                                $$  __$$\           $$ |            $$  __$$\           $$ |    
 $$$$$$$\ $$\   $$\ $$$$$$$\   $$$$$$\   $$$$$$\        $$ /  $$ | $$$$$$$\ $$ |  $$\       $$ |  $$ | $$$$$$\$$$$$$\   
$$  _____|$$ |  $$ |$$  __$$\ $$  __$$\ $$  __$$\       $$$$$$$$ |$$  _____|$$ | $$  |      $$$$$$$\ |$$  __$$\_$$  _|  
$$ /      $$ |  $$ |$$ |  $$ |$$$$$$$$ |$$ |  \__|      $$  __$$ |\$$$$$$\  $$$$$$  /       $$  __$$\ $$ /  $$ |$$ |    
$$ |      $$ |  $$ |$$ |  $$ |$$   ____|$$ |            $$ |  $$ | \____$$\ $$  _$$<        $$ |  $$ |$$ |  $$ |$$ |$$\ 
\$$$$$$$\ \$$$$$$$ |$$$$$$$  |\$$$$$$$\ $$ |            $$ |  $$ |$$$$$$$  |$$ | \$$\       $$$$$$$  |\$$$$$$  |\$$$$  |
 \_______| \____$$ |\_______/  \_______|\__|            \__|  \__|\_______/ \__|  \__|      \_______/  \______/  \____/ 
          $$\   $$ |                                                                                                    
          \$$$$$$  |                                                                                                    
           \______/                                                                                                     

        Cybersecurity Awareness Bot
        ");
    }


     static void PlayVoiceGreeting()
    {
        try
        {
            // Full file path for the sound
            string soundPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greetingVoice.wav");

            // Check if the file exists before attempting to play it
            if (System.IO.File.Exists(soundPath))
            {
                SoundPlayer player = new SoundPlayer(soundPath);
                player.PlaySync();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed; // Dark Red for errors
                Console.WriteLine("[Audio Error] Cannot find the greeting.wav at the path: " + soundPath);
                Console.ResetColor();
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed; // Dark Red for errors
            Console.WriteLine("[Audio Error] Unable to play the greeting sound: " + ex.Message);
            Console.ResetColor();
        }
    }
    } 
} 