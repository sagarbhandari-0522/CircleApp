// On page load or when changing themes, best to add inline in `head` to avoid FOUC
if (localStorage.theme === 'dark' || (!('theme' in localStorage) && window.matchMedia('(prefers-color-scheme: dark)').matches)) {
    document.documentElement.classList.add('dark')
} else {
    document.documentElement.classList.remove('dark')
}

// Whenever the user explicitly chooses light mode
localStorage.theme = 'light'

// Whenever the user explicitly chooses dark mode
localStorage.theme = 'dark'

// Whenever the user explicitly chooses to respect the OS preference
localStorage.removeItem('theme')


document.addEventListener("DOMContentLoaded", function () {
    function bindCommentForm() {
        //Like post
        document.querySelectorAll('.like-form').forEach(form => {
            form.addEventListener('submit', async function (e) {
                e.preventDefault();
                const postId = form.dataset.postId;
                const formData = new FormData();
                formData.append("postId", postId);
                const response = await fetch(form.action, {
                    method: "POST",
                    body: formData,
                });
                console.log(response);
                if (response.ok) {
                    var icon = form.querySelector('ion-icon');
                    var button = form.querySelector('button');
                    var data = await response.json();
                    if (data.isLiked) {
                        icon.setAttribute('name', 'heart');
                        button.classList.add("text-red-500", "bg-red-100");
                    }
                    else {
                        icon.setAttribute("name", "heart-outline");
                        button.classList.remove("text-red-500", "bg-red-100")
                    }
                    console.log(data.likeCount);
                    form.nextElementSibling.textContent = data.likeCount;

                }
                else {
                    console.log("Like is failed");
                }
            });
        });

        // Favorite Post
        document.querySelectorAll(".favorite-form").forEach(form => {
            form.addEventListener("submit", async (e) => {
                e.preventDefault();
                const PostId = form.dataset.postId;
                console.log(PostId);
                const formData = new FormData();
                // const token=document.querySelector('input[name="__RequestVerificationToken"]')
                formData.append("PostId", PostId);
                const response = await fetch(form.action, {
                    method: "POST",
                    body: formData
                });
                if (response.ok) {
                    const icon = form.querySelector('ion-icon');
                    const button = form.querySelector('button');
                    const result = await response.json();
                    if (result.isFavorite) {
                        icon.setAttribute("name", "bookmark");
                        button.classList.add("text-orange-500", "bg-orange-100");
                    }
                    else {
                        icon.setAttribute("name", "bookmark-outline");
                        button.classList.remove("text-orange-500", "bg-orange-100")
                    }
                    console.log(result.favoriteCount);
                    form.nextElementSibling.textContent = result.favoriteCount;

                }


            })
        });
        // add comment
        document.querySelectorAll('.comment-form').forEach(form => {
            form.addEventListener("submit", async (e) => {
                e.preventDefault();
                console.log("sandhya");
                const postId = form.dataset.postId;
                console.log(postId);
                var formData = new FormData();
                var content = form.querySelector("textarea").value;
                formData.append("PostId", postId);
                formData.append("content", content);
                const response = await fetch(form.action, {
                    method: "POST",
                    body: formData,
                });
                if (response.ok) {
                    var html = await response.text();
                    var container = document.getElementById(`post-container-${postId}`);
                    container.innerHTML = html;
                    bindCommentForm();

                }
                else {
                    console.log("Failed to comment");
                }


            });
        });

        //delete comment
        document.querySelectorAll(".comment-delete-form").forEach(form => {
            form.addEventListener("submit", async (e) => {
                e.preventDefault();
                const commentId = form.dataset.commentId;
                console.log(commentId);
                const formData = new FormData();
                formData.append("commentId", commentId);
                const response = await fetch(form.action, {
                    method: "POST",
                    body: formData
                });
                if (response.ok) {
                    const data = await response.json();
                    if (data.success) {
                        var commentSection = form.closest(".comment-item");
                        commentSection.remove();
                    }
                    else {
                        console.log("Unable to delete comment");
                    }
                }

            })

        })
    }
    bindCommentForm();
    function openPostDeleteConfirmation(postId) {
        console.log(postId);
        UIkit.dropdown('.post-options-dropdown').hide();
        document.getElementById('deleteConfirmationPostId').value = postId;
        UIkit.modal('#postDeleteDialog').show();
    }

});


