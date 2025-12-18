using System;
using System.Collections.Generic;

namespace SistemaEducativo912.Models;

public partial class Matricula
{
    public int MatriculaId { get; set; }

    public int EstudianteId { get; set; }

    public int CursoId { get; set; }

    public DateOnly FechaMatricula { get; set; }

    public virtual Curso Curso { get; set; } = null!;

    public virtual Estudiante Estudiante { get; set; } = null!;
}
