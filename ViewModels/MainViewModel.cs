using ElectionData.ViewModels;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using ElectionData.Models;
using System.Globalization;

namespace ElectionData.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
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

        public MainViewModel()
        {
            LoadData();
        }

        private void LoadData()
        {
            var jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data", "candidates.json");
            var json = File.ReadAllText(jsonFilePath);
            var candidatesList = JsonConvert.DeserializeObject<CandidatesWrapper>(json);
            var totalVotes = candidatesList.Candidates.Sum(c => c.Votes);
            foreach (var candidate in candidatesList.Candidates)
            {
                // Update property type and calculation
                candidate.Percentage = (decimal)candidate.Votes / totalVotes;
                decimal precisePercentage = Math.Round(candidate.Percentage * 100, 6);
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
