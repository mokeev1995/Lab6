using Lab6.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab6
{
	public class PostgresContext : DbContext
	{
		public virtual DbSet<AcademicRecord> AcademicRecord { get; set; }
		public virtual DbSet<Curriculum> Curriculum { get; set; }
		public virtual DbSet<Department> Department { get; set; }
		public virtual DbSet<ExaminationList> ExaminationList { get; set; }
		public virtual DbSet<Person> Person { get; set; }
		public virtual DbSet<Professor> Professor { get; set; }
		public virtual DbSet<ProfessorSubject> ProfessorSubject { get; set; }
		public virtual DbSet<Sheet> Sheet { get; set; }
		public virtual DbSet<Specialty> Specialty { get; set; }
		public virtual DbSet<Student> Student { get; set; }
		public virtual DbSet<Studentsgroup> Studentsgroup { get; set; }
		public virtual DbSet<Subject> Subject { get; set; }

		public PostgresContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<AcademicRecord>(entity =>
			{
				entity.ToTable("academicrecord");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.Date)
					.HasColumnName("date")
					.HasColumnType("date")
					.HasDefaultValueSql("('now'::text)::date");

				entity.Property(e => e.Result).HasColumnName("result");

				entity.Property(e => e.Sheet).HasColumnName("sheet");

				entity.HasOne(d => d.SheetNavigation)
					.WithMany(p => p.AcademicRecord)
					.HasForeignKey(d => d.Sheet)
					.HasConstraintName("academicrecord_sheet_fkey");
			});

			modelBuilder.Entity<Curriculum>(entity =>
			{
				entity.ToTable("curriculum");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.Course).HasColumnName("course");

				entity.Property(e => e.HasExam)
					.HasColumnName("has_exam")
					.HasDefaultValueSql("false");

				entity.Property(e => e.HoursAmount).HasColumnName("hours_amount");

				entity.Property(e => e.ProfessorId).HasColumnName("professor_id");

				entity.Property(e => e.Semester).HasColumnName("semester");

				entity.Property(e => e.Specialty).HasColumnName("specialty");

				entity.Property(e => e.Subject).HasColumnName("subject");

				entity.HasOne(d => d.Professor)
					.WithMany(p => p.Curriculum)
					.HasForeignKey(d => d.ProfessorId)
					.HasConstraintName("curriculum_professor_id_fkey");

				entity.HasOne(d => d.SpecialtyNavigation)
					.WithMany(p => p.Curriculum)
					.HasForeignKey(d => d.Specialty)
					.HasConstraintName("curriculum_specialty_fkey");

				entity.HasOne(d => d.SubjectNavigation)
					.WithMany(p => p.Curriculum)
					.HasForeignKey(d => d.Subject)
					.HasConstraintName("curriculum_subject_fkey");
			});

			modelBuilder.Entity<Department>(entity =>
			{
				entity.ToTable("department");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.Name)
					.IsRequired()
					.HasColumnName("name");
			});

			modelBuilder.Entity<ExaminationList>(entity =>
			{
				entity.ToTable("examinationlist");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.Date)
					.HasColumnName("date")
					.HasColumnType("date")
					.HasDefaultValueSql("('now'::text)::date");

				entity.Property(e => e.Result).HasColumnName("result");

				entity.Property(e => e.Sheet).HasColumnName("sheet");

				entity.HasOne(d => d.SheetNavigation)
					.WithMany(p => p.ExaminationList)
					.HasForeignKey(d => d.Sheet)
					.HasConstraintName("examinationlist_sheet_fkey");
			});

			modelBuilder.Entity<Person>(entity =>
			{
				entity.ToTable("person");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.Middlename).HasColumnName("middlename");

				entity.Property(e => e.Name)
					.IsRequired()
					.HasColumnName("name");

				entity.Property(e => e.Surname)
					.IsRequired()
					.HasColumnName("surname");
			});

			modelBuilder.Entity<Professor>(entity =>
			{
				entity.ToTable("professor");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.Department).HasColumnName("department");

				entity.Property(e => e.Person).HasColumnName("person");

				entity.HasOne(d => d.DepartmentNavigation)
					.WithMany(p => p.Professor)
					.HasForeignKey(d => d.Department)
					.HasConstraintName("professor_department_fkey");

				entity.HasOne(d => d.PersonNavigation)
					.WithMany(p => p.Professor)
					.HasForeignKey(d => d.Person)
					.HasConstraintName("professor_person_fkey");
			});

			modelBuilder.Entity<ProfessorSubject>(entity =>
			{
				entity.ToTable("professorsubject");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.Professor).HasColumnName("professor");

				entity.Property(e => e.Subject).HasColumnName("subject");

				entity.HasOne(d => d.ProfessorNavigation)
					.WithMany(p => p.ProfessorSubject)
					.HasForeignKey(d => d.Professor)
					.HasConstraintName("professorsubject_professor_fkey");

				entity.HasOne(d => d.SubjectNavigation)
					.WithMany(p => p.ProfessorSubject)
					.HasForeignKey(d => d.Subject)
					.HasConstraintName("professorsubject_subject_fkey");
			});

			modelBuilder.Entity<Sheet>(entity =>
			{
				entity.ToTable("sheet");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.Curriculum).HasColumnName("curriculum");

				entity.Property(e => e.Date)
					.HasColumnName("date")
					.HasColumnType("date");

				entity.Property(e => e.Student).HasColumnName("student");

				entity.HasOne(d => d.CurriculumNavigation)
					.WithMany(p => p.Sheet)
					.HasForeignKey(d => d.Curriculum)
					.HasConstraintName("sheet_curriculum_fkey");

				entity.HasOne(d => d.StudentNavigation)
					.WithMany(p => p.Sheet)
					.HasForeignKey(d => d.Student)
					.HasConstraintName("sheet_student_fkey");
			});

			modelBuilder.Entity<Specialty>(entity =>
			{
				entity.ToTable("specialty");

				entity.HasIndex(e => e.Name)
					.HasName("specialty_name_key")
					.IsUnique();

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.Name)
					.IsRequired()
					.HasColumnName("name");
			});

			modelBuilder.Entity<Student>(entity =>
			{
				entity.ToTable("student");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.Person).HasColumnName("person");

				entity.Property(e => e.StudentsGroup).HasColumnName("students_group");

				entity.HasOne(d => d.PersonNavigation)
					.WithMany(p => p.Student)
					.HasForeignKey(d => d.Person)
					.HasConstraintName("student_person_fkey");

				entity.HasOne(d => d.StudentsGroupNavigation)
					.WithMany(p => p.Student)
					.HasForeignKey(d => d.StudentsGroup)
					.HasConstraintName("student_students_group_fkey");
			});

			modelBuilder.Entity<Studentsgroup>(entity =>
			{
				entity.ToTable("studentsgroup");

				entity.HasIndex(e => e.Name)
					.HasName("studentsgroup_name_key")
					.IsUnique();

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.Course).HasColumnName("course");

				entity.Property(e => e.DateFinish)
					.HasColumnName("date_finish")
					.HasColumnType("date");

				entity.Property(e => e.DateStart)
					.HasColumnName("date_start")
					.HasColumnType("date");

				entity.Property(e => e.Name)
					.IsRequired()
					.HasColumnName("name");

				entity.Property(e => e.Specialty).HasColumnName("specialty");

				entity.HasOne(d => d.SpecialtyNavigation)
					.WithMany(p => p.StudentsGroup)
					.HasForeignKey(d => d.Specialty)
					.HasConstraintName("studentsgroup_specialty_fkey");
			});

			modelBuilder.Entity<Subject>(entity =>
			{
				entity.ToTable("subject");

				entity.Property(e => e.Id).HasColumnName("id");

				entity.Property(e => e.Name)
					.IsRequired()
					.HasColumnName("name");
			});
		}
	}
}