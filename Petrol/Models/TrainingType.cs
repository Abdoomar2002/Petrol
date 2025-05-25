
using System.Collections.Generic;

public class TrainingType
{
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Training> Trainings { get; set; }
}
