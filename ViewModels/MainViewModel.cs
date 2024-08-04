using ElectionData.ViewModels;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using ElectionData.Models;
using System.Globalization;
using System.Linq; // Make sure to include this namespace

namespace ElectionData.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private const decimal RegisteredVoters = 21392464; // Constant for registered voters

        private ObservableCollection<Candidate> candidates;
        public ObservableCollection<Candidate> Candidates
        {
            get { return candidates; }
            set
            {
                candidates = value;
                OnPropertyChanged(nameof(Candidates));
            }
        }

        private decimal totalVotes;
        public decimal TotalVotes
        {
            get { return totalVotes; }
            set
            {
                totalVotes = value;
                OnPropertyChanged(nameof(TotalVotes));
                // Update the TotalVotesPercentage whenever TotalVotes is set
                TotalVotesPercentage = (TotalVotes / RegisteredVoters) * 100;
            }
        }

        private decimal totalVotesPercentage;
        public decimal TotalVotesPercentage
        {
            get { return totalVotesPercentage; }
            set
            {
                totalVotesPercentage = value;
                OnPropertyChanged(nameof(TotalVotesPercentage));
            }
        }

        public MainViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data", "candidates.json");
            var json = File.ReadAllText(jsonFilePath);
            var candidatesList = JsonConvert.DeserializeObject<CandidatesWrapper>(json);
            var totalVotes = candidatesList.Candidates.Sum(c => c.Votes);
            TotalVotes = totalVotes; // Update TotalVotes property
            foreach (var candidate in candidatesList.Candidates)
            {
                // Update property type and calculation
                candidate.Percentage = (decimal)candidate.Votes / totalVotes;
                decimal precisePercentage = Math.Round(candidate.Percentage * 100, 5);
                candidate.FormattedPercentage = precisePercentage.ToString("F6", CultureInfo.InvariantCulture);
            }
            Candidates = new ObservableCollection<Candidate>(candidatesList.Candidates);
        }
    }

    public class CandidatesWrapper
    {
        public List<Candidate> Candidates { get; set; }
    }
}
