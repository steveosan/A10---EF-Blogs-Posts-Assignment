using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace dotnet_inclass_10
{
    class Program
    {
        static void Main(string[] args)
        {
            string choice;

            do
            {
                // ask user a question
                    //Menu//
                System.Console.WriteLine("\n ####  Main Menu  ####");
                Console.WriteLine("1) Display all blogs");
                Console.WriteLine("2) Add new blog");
                System.Console.WriteLine("3) Create Post");
                System.Console.WriteLine("4) Display Posts");
                Console.WriteLine("Enter any other key to exit.");
                System.Console.Write("User Input: ");


                // input response
                choice = Console.ReadLine();
                // read data from file
                if (choice == "1")
                {
                // read data from file
                using (var db = new BlogContext())
                    {
                        System.Console.WriteLine("\n   Here is the list of Blogs");
                        System.Console.WriteLine("################################\n");
                        foreach (var b in db.Blogs)
                        {
                            System.Console.WriteLine($"Blog: {b.BlogId}:  {b.Name}");
                        }
                    }
                }
                //Create new Blog
                if (choice == "2")
                {
                    System.Console.WriteLine("enter your blog name");
                    var BlogName = Console.ReadLine();

                    
                    var blog = new Blog();
                    blog.Name = BlogName;

                    //Save new Blog object to database
                    using (var db = new BlogContext())
                    {
                    db.Add(blog);
                    db.SaveChanges();
                    }
                }
                // Add post to database
                if (choice == "3")
                {
                    
                    System.Console.WriteLine("Please select which Blog this post belongs to");
                    using (var db = new BlogContext())
                    {
                        foreach (var b in db.Blogs)
                        {
                            System.Console.WriteLine($"Blog: {b.BlogId}:  {b.Name}");
                        }
                    }
                    //Pick which blog
                    System.Console.Write("User Input: ");
                    var PostBlogId = Convert.ToInt32(Console.ReadLine());
                    //Add post name 
                    System.Console.WriteLine("enter your post name");
                    var postTitle = Console.ReadLine();
                    // Add post content
                    System.Console.WriteLine("Add some content to this post");
                    var postContent = Console.ReadLine();

                    var post = new Post();
                    post.Title = postTitle;
                    post.BlogId = PostBlogId;
                    post.Content = postContent;
                    
                    //Save Post
                    using (var db = new BlogContext())
                    {
                        db.Posts.Add(post);
                        db.SaveChanges();
                    }
                }
                //Display posts
                if (choice == "4")
                {
                    int count = 0;
                    
                    System.Console.WriteLine("Select the blog's post to display: ");
                    System.Console.WriteLine($"{count}) Posts from: All blogs");
                    using (var db = new BlogContext())
                    {
                        foreach (var b in db.Blogs)
                        {
                            count++;
                            System.Console.WriteLine($"{b.BlogId}) Posts from: {b.Name}");
                        }
                    }
                //Try to print the selection
                System.Console.Write("User Input:");
                int filter = Convert.ToInt32(Console.ReadLine());
                    
                        if (filter == 0 )
                        {
                            using (var db = new BlogContext())
                            {
                                var blogPost = db.Blogs
                                    .Join(
                                        db.Posts,
                                        blog => blog.BlogId,
                                        post => post.BlogId,
                                        (blog , Post) => new
                                        {
                                            BlogName = blog.Name,
                                            BlogId = blog.BlogId,
                                            BlogPost = Post.Title,
                                            BlogContent = Post.Content
                                        }
                                    ).ToList();

                                    foreach(var blog in blogPost)
                                    {
				                        Console.WriteLine("Blog Name: {0} \n\t Post Title: {1} \n\t Post Content: {2} ", blog.BlogName, blog.BlogPost, blog.BlogContent);
                                    }
                            }
                        // try to apply filter to BlogId 
                        }else if (filter != 0 )
                            using (var db = new BlogContext())
                            {
                                
                                var blogPost = db.Blogs
                                    .Where(b => b.BlogId == filter)
                                    .Join(
                                        db.Posts,
                                        blog => blog.BlogId,
                                        post => post.BlogId,
                                        (blog , Post) => new
                                        {
                                            BlogName = blog.Name,
                                            BlogId = blog.BlogId,
                                            BlogPost = Post.Title,
                                            BlogContent = Post.Content
                                        }

                                    ).ToList();

                                    foreach(var blog in blogPost)
                                    {
				                        Console.WriteLine("Blog Name: {0} \n\t Post Title: {1} \n\t Post Content: {2} ", blog.BlogName, blog.BlogPost, blog.BlogContent);
                                    }
                            }
                }
            }while (choice == "1" || choice == "2" || choice == "3" || choice == "4");
        }
    }
}




