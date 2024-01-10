namespace XPathXQuery.Models;

using Ookii.Dialogs.Wpf;

public class Repository
{
    static XmlDocument docPeople = new XmlDocument();
    static XmlDocument docMovies = new XmlDocument();
    static XmlDocument docOscars = new XmlDocument();

    public Repository()
    {
    }

    public static string GetFullPathApp() => AppDomain.CurrentDomain.BaseDirectory[..^1];

    public static string? SelectPath(string title = "")
    {
        VistaFolderBrowserDialog openFileDialog = new VistaFolderBrowserDialog();
        if (!string.IsNullOrEmpty(title))
        {
            openFileDialog.Description = title;
            openFileDialog.UseDescriptionForTitle = true;
        }
        if (openFileDialog.ShowDialog() ?? false)
            return openFileDialog.SelectedPath;
        return null;
    }

    public static void Connect()
    {
        try
        {
            string filePeople = $"{GetFullPathApp()}/Resources/people.xml";
            if (File.Exists(filePeople))
            {
                docPeople.Load(filePeople);
            }
            else
            {
                Dialogs.GenerateMessage(MessageBoxImage.Error, "File people.xml not found!");
            }
            string fileMovies = $"{GetFullPathApp()}/Resources/movies.xml";
            if (File.Exists(fileMovies))
            {
                docMovies.Load(fileMovies);
            }
            else
            {
                Dialogs.GenerateMessage(MessageBoxImage.Error, "File movies.xml not found!");
            }
            string fileOscars = $"{GetFullPathApp()}/Resources/oscars.xml";
            if (File.Exists(fileOscars))
            {
                docOscars.Load(fileOscars);
            }
            else
            {
                Dialogs.GenerateMessage(MessageBoxImage.Error, "File oscars.xml not found!");
            }
        }
        catch (Exception)
        {
            // Dialogs.GenerateMessage(MessageBoxImage.Error, "File in use!").
            Application.Current.Shutdown();
        }
    }

    public static List<T> FromIListToList<T>(System.Collections.IList iList) => iList.Cast<T>().ToList();

    public T? Deserialize<T>(XmlNode node)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        StringReader reader = new StringReader(node.OuterXml);
        T? result = (T?)serializer.Deserialize(reader);
        return result;
    }

    public List<Person> SearchPerson(string name = "")
    {
        List<Person> lpeople = new List<Person>();
        XmlNodeList? nodeList = docPeople.SelectNodes($"//person[contains(translate(name[text()], '{name.ToLower()}', '{name.ToUpper()}'), '{name.ToUpper()}')]");
        lpeople = AllPerson(nodeList, lpeople);
        lpeople = lpeople.OrderBy(x => x.Name).ToList();
        return lpeople;
    }
    public Person? SearchPerson(Oscar oscar)
    {
        List<Person> lpeople = new List<Person>();
        XmlNodeList? nodeList = docPeople.SelectNodes($"//person[_id = '{oscar.Person?.Id}']");
        lpeople = AllPerson(nodeList, lpeople);
        if (lpeople.Count > 0)
        {
            return lpeople.FirstOrDefault();
        }
        return null;
    }

    public List<Movie> SearchMovie(string title = "")
    {
        List<Movie> lmovies = new List<Movie>();
        XmlNodeList? nodeList = docMovies.SelectNodes($"//movie[contains(translate(name[text()], '{title.ToLower()}', '{title.ToUpper()}'), '{title.ToUpper()}')]");
        lmovies = AllMovie(nodeList, lmovies);
        lmovies = lmovies.OrderBy(x => x.Name).ToList();
        return lmovies;
    }

    public Movie? SearchMovie(Oscar oscar)
    {
        List<Movie> lmovies = new List<Movie>();
        XmlNodeList? nodeList = docMovies.SelectNodes($"//movie[_id = '{oscar.Movie?.Id}']");
        lmovies = AllMovie(nodeList, lmovies);
        if (lmovies.Count != 0)
        {
            return lmovies.FirstOrDefault();
        }
        return null;
    }

    public List<Oscar> SearchOscars(string year = "", string type = "")
    {
        List<Oscar> loscars = new List<Oscar>();
        XmlNodeList? nodeList = docOscars.SelectNodes($"//oscar[contains(translate(year[text()], '{year.ToLower()}', '{year.ToUpper()}'), '{year.ToUpper()}') and contains(translate(type[text()], '{type.ToLower()}', '{type.ToUpper()}'), '{type.ToUpper()}')]");
        loscars = AllOscars(nodeList, loscars);
        loscars = loscars.OrderBy(x => x.Movie?.Name).ThenBy(x => x.Person?.Name).ToList();
        return loscars;
    }

    public List<Person> AllPerson(XmlNodeList? nodeList, List<Person> lpeople)
    {
        if (nodeList?.Count > 0)
        {
            foreach (XmlNode node in nodeList)
            {
                Person? person = Deserialize<Person>(node);
                if (person != null)
                {
                    lpeople.Add(person);
                }
            }
        }
        return lpeople;
    }

    public List<Person> AllPerson(List<Movie> lmovies)
    {
        List<Person> lpeople = new List<Person>();
        List<string> lids = lmovies.Select(x => x.Actors.Select(x => x.Id)).SelectMany(x => x).Distinct().ToList();
        lids.AddRange(lmovies.Select(x => x.Directors.Select(x => x.Id)).SelectMany(x => x).Distinct().ToList());
        lids = lids.Distinct().ToList();
        foreach (string id in lids)
        {
            XmlNodeList? nodeList = docPeople.SelectNodes($"//person[_id = '{id}']");
            lpeople = AllPerson(nodeList, lpeople);
        }
        lpeople = lpeople.OrderBy(x => x.Name).ToList();
        return lpeople;
    }

    public List<Person> AllPerson(List<Oscar> loscars)
    {
        List<Person> lpeople = new List<Person>();
        List<string?> lids = loscars.Where(x => x.Person != null).Select(x => x.Person).Select(x => x?.Id).Distinct().ToList();

        foreach (string? id in lids)
        {
            XmlNodeList? nodeList = docPeople.SelectNodes($"//person[_id = '{id}']");
            lpeople.AddRange(AllPerson(nodeList, new List<Person>()));
        }
        lpeople = lpeople.OrderBy(x => x.Name).ToList();
        return lpeople;
    }

    public List<Movie> AllMovie(List<Person> lpeople)
    {
        List<Movie> lmovies = new List<Movie>();
        foreach (string personId in lpeople.Select(x => x.Id))
        {
            XmlNodeList? nodeList = docMovies.SelectNodes($"//movie[actors/id = '{personId}']");
            lmovies = AllMovie(nodeList, lmovies);
            nodeList = docMovies.SelectNodes($"//movie[directors/id = '{personId}']");
            lmovies = AllMovie(nodeList, lmovies);
        }
        lmovies = lmovies.OrderBy(x => x.Name).ToList();
        return lmovies;
    }

    public List<Movie> AllMovie(List<Oscar> loscars)
    {
        List<Movie> lmovies = new List<Movie>();
        foreach (Oscar oscar in loscars.Where(x => x.Movie != null))
        {
            XmlNodeList? nodeList = docMovies.SelectNodes($"//movie[_id = '{oscar.Movie?.Id}']");
            lmovies = AllMovie(nodeList, lmovies);
        }
        lmovies = lmovies.OrderBy(x => x.Name).ToList();
        return lmovies;
    }

    public List<Movie> AllMovie(XmlNodeList? nodeList, List<Movie> lmovies)
    {
        if (nodeList?.Count > 0)
        {
            foreach (XmlNode node in nodeList)
            {
                Movie? movie = Deserialize<Movie>(node);
                if (movie != null)
                {
                    AddActors(movie);
                    AddDirectors(movie);
                    if (!lmovies.Contains(movie))
                    {
                        lmovies.Add(movie);
                    }
                }
            }
        }
        return lmovies;
    }

    private void AddActors(Movie movie)
    {
        XmlNodeList? nodeList = docMovies.SelectNodes($"//movie[./_id = '{movie.Id}']/actors");
        if (nodeList?.Count > 0)
        {
            foreach (XmlNode node in nodeList)
            {
                Actor? actor = Deserialize<Actor>(node);
                if (actor != null)
                {
                    movie.Actors.Add(actor);
                }
            }
        }
    }

    private void AddDirectors(Movie movie)
    {
        XmlNodeList? nodeList = docMovies.SelectNodes($"//movie[./_id = '{movie.Id}']/directors");
        if (nodeList?.Count > 0)
        {
            foreach (XmlNode node in nodeList)
            {
                Director? director = Deserialize<Director>(node);
                if (director != null)
                {
                    movie.Directors.Add(director);
                }
            }
        }
    }

    public List<Oscar> AllOscars(XmlNodeList? nodeList, List<Oscar> loscars)
    {
        if (nodeList?.Count > 0)
        {
            foreach (XmlNode node in nodeList)
            {
                Oscar? oscar = Deserialize<Oscar>(node);
                if (oscar != null && !loscars.Contains(oscar))
                {
                    loscars.Add(oscar);
                }
            }
        }
        return loscars;
    }

    public List<Oscar> AllOscars(List<Models.Person> lpeople)
    {
        List<Oscar> loscars = new List<Oscar>();
        foreach (Person person in lpeople)
        {
            XmlNodeList? nodeList = docOscars.SelectNodes($"//oscar[person/id = '{person.Id}']");
            loscars = AllOscars(nodeList, loscars);
        }
        loscars = loscars.OrderBy(x => x.Movie?.Name).ThenBy(x => x.Person?.Name).ToList();
        return loscars;
    }

    public List<Oscar> AllOscars(List<Models.Movie> lmovies)
    {
        List<Oscar> loscars = new List<Oscar>();
        foreach (Movie movie in lmovies)
        {
            XmlNodeList? nodeList = docOscars.SelectNodes($"//oscar[movie/id = '{movie.Id}']");
            loscars.AddRange(AllOscars(nodeList, new List<Oscar>()));
        }
        loscars = loscars.OrderBy(x => x.Movie?.Name).ThenBy(x => x.Person?.Name).ToList();
        return loscars;
    }

    public void GenerateFile(List<Models.Oscar> loscars, string path)
    {
        XmlDocument doc = new XmlDocument();
        XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
        XmlElement? root = doc.DocumentElement;
        doc.InsertBefore(xmlDeclaration, root);

        XmlElement oscars = doc.CreateElement(string.Empty, "oscars", string.Empty);
        doc.AppendChild(oscars);

        foreach (Oscar o in loscars)
        {
            XmlElement oscar = doc.CreateElement(string.Empty, "oscar", string.Empty);

            XmlElement year = doc.CreateElement(string.Empty, "year", string.Empty);
            XmlText yearc = doc.CreateTextNode(o.Year.ToString());
            year.AppendChild(yearc);
            oscar.AppendChild(year);

            XmlElement type = doc.CreateElement(string.Empty, "type", string.Empty);
            XmlText typec = doc.CreateTextNode(o.Type);
            type.AppendChild(typec);
            oscar.AppendChild(type);

            AddPersonToOscar(doc, o, oscar);
            AddMovieToOscar(doc, o, oscar);

            oscars.AppendChild(oscar);
        }

        doc.Save($"{path}/xmlgenerat.xml");
    }

    private void AddPersonToOscar(XmlDocument doc, Oscar o, XmlElement oscar)
    {
        if (o.Person != null)
        {
            Person? p = SearchPerson(o);
            if (p != null)
            {
                XmlElement person = doc.CreateElement(string.Empty, "person", string.Empty);

                XmlElement id = doc.CreateElement(string.Empty, "_id", string.Empty);
                XmlText idc = doc.CreateTextNode(p.Id);
                id.AppendChild(idc);
                person.AppendChild(id);

                XmlElement name = doc.CreateElement(string.Empty, "name", string.Empty);
                XmlText namec = doc.CreateTextNode(p.Name);
                name.AppendChild(namec);
                person.AppendChild(name);

                XmlElement dob = doc.CreateElement(string.Empty, "dob", string.Empty);
                XmlText dobc = doc.CreateTextNode(p.Dob);
                dob.AppendChild(dobc);
                person.AppendChild(dob);

                XmlElement pob = doc.CreateElement(string.Empty, "pob", string.Empty);
                XmlText pobc = doc.CreateTextNode(p.Pob);
                pob.AppendChild(pobc);
                person.AppendChild(pob);

                XmlElement hasActed = doc.CreateElement(string.Empty, "hasActed", string.Empty);
                XmlText hasActedc = doc.CreateTextNode(p.HasActed.ToString());
                hasActed.AppendChild(hasActedc);
                person.AppendChild(hasActed);

                XmlElement hasDirected = doc.CreateElement(string.Empty, "hasDirected", string.Empty);
                XmlText hasDirectedc = doc.CreateTextNode(p.HasDirected.ToString());
                hasDirected.AppendChild(hasDirectedc);
                person.AppendChild(hasDirected);

                oscar.AppendChild(person);
            }
        }
    }

    private void AddMovieToOscar(XmlDocument doc, Oscar o, XmlElement oscar)
    {
        if (o.Movie != null)
        {
            Movie? m = SearchMovie(o);
            if (m != null)
            {
                XmlElement movie = doc.CreateElement(string.Empty, "movie", string.Empty);

                XmlElement id = doc.CreateElement(string.Empty, "_id", string.Empty);
                XmlText idc = doc.CreateTextNode(m.Id);
                id.AppendChild(idc);
                movie.AppendChild(id);

                XmlElement name = doc.CreateElement(string.Empty, "name", string.Empty);
                XmlText namec = doc.CreateTextNode(m.Name);
                name.AppendChild(namec);
                movie.AppendChild(name);

                XmlElement year1 = doc.CreateElement(string.Empty, "year", string.Empty);
                XmlText year1c = doc.CreateTextNode(m.Year.ToString());
                year1.AppendChild(year1c);
                movie.AppendChild(year1);

                if (m.Rating != null)
                {
                    XmlElement rating = doc.CreateElement(string.Empty, "rating", string.Empty);
                    XmlText ratingc = doc.CreateTextNode(m.Rating.ToString());
                    rating.AppendChild(ratingc);
                    movie.AppendChild(rating);
                }

                XmlElement runtime = doc.CreateElement(string.Empty, "runtime", string.Empty);
                XmlText runtimec = doc.CreateTextNode(m.Runtime.ToString());
                runtime.AppendChild(runtimec);
                movie.AppendChild(runtime);

                XmlElement genre = doc.CreateElement(string.Empty, "genre", string.Empty);
                XmlText genrec = doc.CreateTextNode(m.Genre);
                genre.AppendChild(genrec);
                movie.AppendChild(genre);

                XmlElement earnings_rank = doc.CreateElement(string.Empty, "earnings_rank", string.Empty);
                XmlText earnings_rankc = doc.CreateTextNode(m.Earnings_rank.ToString());
                earnings_rank.AppendChild(earnings_rankc);
                movie.AppendChild(earnings_rank);

                AddActorsToMovie(doc, m, movie);
                AddDirectorsToMovie(doc, m, movie);

                oscar.AppendChild(movie);
            }
        }
    }

    private void AddActorsToMovie(XmlDocument doc, Movie m, XmlElement movie)
    {
        if (m.Actors?.Count > 0)
        {
            foreach (Actor a in m.Actors)
            {
                XmlElement actors = doc.CreateElement(string.Empty, "actors", string.Empty);

                XmlElement id1 = doc.CreateElement(string.Empty, "id", string.Empty);
                XmlText id1c = doc.CreateTextNode(a.Id);
                id1.AppendChild(id1c);
                actors.AppendChild(id1);

                XmlElement name1 = doc.CreateElement(string.Empty, "name", string.Empty);
                XmlText name1c = doc.CreateTextNode(a.Name);
                name1.AppendChild(name1c);
                actors.AppendChild(name1);

                movie.AppendChild(actors);
            }
        }
    }

    private void AddDirectorsToMovie(XmlDocument doc, Movie m, XmlElement movie)
    {
        if (m.Directors?.Count > 0)
        {
            foreach (Director d in m.Directors)
            {
                XmlElement directors = doc.CreateElement(string.Empty, "directors", string.Empty);

                XmlElement id1 = doc.CreateElement(string.Empty, "id", string.Empty);
                XmlText id1c = doc.CreateTextNode(d.Id);
                id1.AppendChild(id1c);
                directors.AppendChild(id1);

                XmlElement name1 = doc.CreateElement(string.Empty, "name", string.Empty);
                XmlText name1c = doc.CreateTextNode(d.Name);
                name1.AppendChild(name1c);
                directors.AppendChild(name1);

                movie.AppendChild(directors);
            }
        }
    }

    public void InsertPerson(Person p)
    {
        XmlElement? people = docPeople.DocumentElement;
        if (p != null)
        {
            XmlElement person = docPeople.CreateElement(string.Empty, "person", string.Empty);

            XmlElement id = docPeople.CreateElement(string.Empty, "_id", string.Empty);
            XmlText idc = docPeople.CreateTextNode(p.Id);
            id.AppendChild(idc);
            person.AppendChild(id);

            XmlElement name = docPeople.CreateElement(string.Empty, "name", string.Empty);
            XmlText namec = docPeople.CreateTextNode(p.Name);
            name.AppendChild(namec);
            person.AppendChild(name);

            XmlElement dob = docPeople.CreateElement(string.Empty, "dob", string.Empty);
            XmlText dobc = docPeople.CreateTextNode(p.Dob);
            dob.AppendChild(dobc);
            person.AppendChild(dob);

            XmlElement pob = docPeople.CreateElement(string.Empty, "pob", string.Empty);
            XmlText pobc = docPeople.CreateTextNode(p.Pob);
            pob.AppendChild(pobc);
            person.AppendChild(pob);

            XmlElement hasActed = docPeople.CreateElement(string.Empty, "hasActed", string.Empty);
            XmlText hasActedc = docPeople.CreateTextNode(p.HasActed.ToString().ToLower());
            hasActed.AppendChild(hasActedc);
            person.AppendChild(hasActed);

            XmlElement hasDirected = docPeople.CreateElement(string.Empty, "hasDirected", string.Empty);
            XmlText hasDirectedc = docPeople.CreateTextNode(p.HasDirected.ToString().ToLower());
            hasDirected.AppendChild(hasDirectedc);
            person.AppendChild(hasDirected);

            people?.AppendChild(person);

            docPeople.Save("./Resources/people.xml");
        }
    }

    public void DeletePerson(Person person)
    {
        XmlElement? delete = (XmlElement?)docPeople.SelectSingleNode($"//person[_id = '{person.Id}']");
        if (delete != null)
        {
            delete.ParentNode?.RemoveChild(delete);
            docPeople.Save("./Resources/people.xml");
        }
    }

    public void UpdatePerson(Person person, string idOld)
    {
        XmlElement? element = (XmlElement?)docPeople.SelectSingleNode($"//person[_id = '{idOld}']");
        if (element != null)
        {
            XmlElement? node1 = (XmlElement?)docPeople.SelectSingleNode($"//person[_id = '{idOld}']/name");
            if (node1 != null)
            {
                node1.InnerText = person.Name;
            }
            XmlElement? node2 = (XmlElement?)docPeople.SelectSingleNode($"//person[_id = '{idOld}']/dob");
            if (node2 != null)
            {
                node2.InnerText = person.Dob;
            }
            XmlElement? node3 = (XmlElement?)docPeople.SelectSingleNode($"//person[_id = '{idOld}']/pob");
            if (node3 != null)
            {
                node3.InnerText = person.Pob;
            }
            XmlElement? node4 = (XmlElement?)docPeople.SelectSingleNode($"//person[_id = '{idOld}']/hasActed");
            if (node4 != null)
            {
                node4.InnerText = person.HasActed.ToString().ToLower();
            }
            else
            {
                XmlElement hasActed = docPeople.CreateElement(string.Empty, "hasActed", string.Empty);
                XmlText hasActedc = docPeople.CreateTextNode(person.HasActed.ToString().ToLower());
                hasActed.AppendChild(hasActedc);
                element.AppendChild(hasActed);
            }
            XmlElement? node5 = (XmlElement?)docPeople.SelectSingleNode($"//person[_id = '{idOld}']/hasDirected");
            if (node5 != null)
            {
                node5.InnerText = person.HasDirected.ToString().ToLower();
            }
            else
            {
                XmlElement hasDirected = docPeople.CreateElement(string.Empty, "hasDirected", string.Empty);
                XmlText hasDirectedc = docPeople.CreateTextNode(person.HasDirected.ToString().ToLower());
                hasDirected.AppendChild(hasDirectedc);
                element.AppendChild(hasDirected);
            }
            XmlElement? node = (XmlElement?)docPeople.SelectSingleNode($"//person[_id = '{idOld}']/_id");
            if (node != null)
            {
                node.InnerText = person.Id;
            }
            docPeople.Save("./Resources/people.xml");
        }
    }
    public void InsertMovie(Movie m)
    {
        XmlElement? movies = docMovies.DocumentElement;
        if (m != null)
        {
            XmlElement movie = docMovies.CreateElement(string.Empty, "movie", string.Empty);

            XmlElement id = docMovies.CreateElement(string.Empty, "_id", string.Empty);
            XmlText idc = docMovies.CreateTextNode(m.Id);
            id.AppendChild(idc);
            movie.AppendChild(id);

            XmlElement name = docMovies.CreateElement(string.Empty, "name", string.Empty);
            XmlText namec = docMovies.CreateTextNode(m.Name);
            name.AppendChild(namec);
            movie.AppendChild(name);

            if (m.Year != 0)
            {
                XmlElement year = docMovies.CreateElement(string.Empty, "year", string.Empty);
                XmlText yearc = docMovies.CreateTextNode(m.Year.ToString());
                year.AppendChild(yearc);
                movie.AppendChild(year);
            }


            XmlElement rating = docMovies.CreateElement(string.Empty, "rating", string.Empty);
            XmlText ratingc = docMovies.CreateTextNode(m.Rating);
            rating.AppendChild(ratingc);
            movie.AppendChild(rating);

            if (m.Runtime != 0)
            {
                XmlElement runtime = docMovies.CreateElement(string.Empty, "runtime", string.Empty);
                XmlText runtimec = docMovies.CreateTextNode(m.Runtime.ToString());
                runtime.AppendChild(runtimec);
                movie.AppendChild(runtime);
            }

            XmlElement genre = docMovies.CreateElement(string.Empty, "genre", string.Empty);
            XmlText genrec = docMovies.CreateTextNode(m.Genre);
            genre.AppendChild(genrec);
            movie.AppendChild(genre);

            if (m.Earnings_rank != 0)
            {
                XmlElement earnings_rank = docMovies.CreateElement(string.Empty, "earnings_rank", string.Empty);
                XmlText earnings_rankc = docMovies.CreateTextNode(m.Earnings_rank.ToString());
                earnings_rank.AppendChild(earnings_rankc);
                movie.AppendChild(earnings_rank);
            }

            foreach (Actor a in m.Actors)
            {
                XmlElement actors = docMovies.CreateElement(string.Empty, "actors", string.Empty);

                XmlElement id1 = docMovies.CreateElement(string.Empty, "id", string.Empty);
                XmlText id1c = docMovies.CreateTextNode(a.Id);
                id1.AppendChild(id1c);
                actors.AppendChild(id1);

                XmlElement name1 = docMovies.CreateElement(string.Empty, "name", string.Empty);
                XmlText name1c = docMovies.CreateTextNode(a.Name);
                name1.AppendChild(name1c);
                actors.AppendChild(name1);

                movie.AppendChild(actors);
            }

            foreach (Director d in m.Directors)
            {
                XmlElement directors = docMovies.CreateElement(string.Empty, "directors", string.Empty);

                XmlElement id1 = docMovies.CreateElement(string.Empty, "id", string.Empty);
                XmlText id1c = docMovies.CreateTextNode(d.Id);
                id1.AppendChild(id1c);
                directors.AppendChild(id1);

                XmlElement name1 = docMovies.CreateElement(string.Empty, "name", string.Empty);
                XmlText name1c = docMovies.CreateTextNode(d.Name);
                name1.AppendChild(name1c);
                directors.AppendChild(name1);

                movie.AppendChild(directors);
            }

            movies?.AppendChild(movie);

            docMovies.Save("./Resources/movies.xml");
        }
    }

    public void DeleteMovie(Movie movie)
    {
        XmlElement? delete = (XmlElement?)docMovies.SelectSingleNode($"//movie[_id = '{movie.Id}']");
        if (delete != null)
        {
            delete.ParentNode?.RemoveChild(delete);
            docMovies.Save("./Resources/movies.xml");
        }
    }

    public void UpdateMovie(Movie movie, Movie movieOld)
    {
        DeleteMovie(movieOld);
        InsertMovie(movie);
    }

}
